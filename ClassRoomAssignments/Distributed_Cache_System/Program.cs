using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

// Cache entry
public class CacheEntry<TValue> where TValue : class
{
    public TValue Value { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public int Version { get; set; }
    public string NodeId { get; set; }

    public bool IsExpired => ExpiresAt.HasValue && DateTime.Now > ExpiresAt.Value;

    public CacheEntry(TValue value, TimeSpan? ttl = null)
    {
        Value = value;
        CreatedAt = DateTime.Now;
        Version = 1;
        if (ttl.HasValue)
        {
            ExpiresAt = DateTime.Now.Add(ttl.Value);
        }
    }
}

// Cache result
public class CacheResult<TValue> where TValue : class
{
    public bool Found { get; set; }
    public TValue Value { get; set; }
    public string FromNode { get; set; }
    public int Version { get; set; }
    public bool WasRepaired { get; set; }

    public static CacheResult<TValue> Miss()
    {
        return new CacheResult<TValue> { Found = false };
    }

    public static CacheResult<TValue> Hit(TValue value, string nodeId, int version)
    {
        return new CacheResult<TValue>
        {
            Found = true,
            Value = value,
            FromNode = nodeId,
            Version = version
        };
    }
}

// Ring position
public class RingPosition
{
    public string NodeId { get; set; }
    public uint HashValue { get; set; }
    public bool IsVirtual { get; set; }
    public int VirtualIndex { get; set; }
}

// Cache node
public class CacheNode<TKey, TValue>
    where TKey : IEquatable<TKey>
    where TValue : class
{
    public string NodeId { get; set; }
    public ConcurrentDictionary<TKey, CacheEntry<TValue>> Data { get; set; }
    public List<uint> HashPositions { get; set; }
    public bool IsOnline { get; set; }
    public int ItemCount => Data.Count;

    public CacheNode(string nodeId)
    {
        NodeId = nodeId;
        Data = new ConcurrentDictionary<TKey, CacheEntry<TValue>>();
        HashPositions = new List<uint>();
        IsOnline = true;
    }
}

// Bloom filter for key existence
public class BloomFilter
{
    private bool[] bits;
    private int hashCount;
    private int size;

    public BloomFilter(int size, int hashCount)
    {
        this.size = size;
        this.hashCount = hashCount;
        bits = new bool[size];
    }

    public void Add(string item)
    {
        for (int i = 0; i < hashCount; i++)
        {
            int index = GetHash(item, i);
            bits[index] = true;
        }
    }

    public bool MayContain(string item)
    {
        for (int i = 0; i < hashCount; i++)
        {
            int index = GetHash(item, i);
            if (!bits[index])
            {
                return false;
            }
        }
        return true;
    }

    private int GetHash(string item, int seed)
    {
        using (MD5 md5 = MD5.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(item + seed);
            byte[] hash = md5.ComputeHash(bytes);
            int value = BitConverter.ToInt32(hash, 0);
            return Math.Abs(value) % size;
        }
    }
}

// Distributed cache with consistent hashing
public class DistributedCache<TKey, TValue> : IDisposable
    where TKey : IEquatable<TKey>
    where TValue : class
{
    private SortedDictionary<uint, CacheNode<TKey, TValue>> hashRing = new SortedDictionary<uint, CacheNode<TKey, TValue>>();
    private ConcurrentDictionary<string, CacheNode<TKey, TValue>> nodes = new ConcurrentDictionary<string, CacheNode<TKey, TValue>>();
    private ReaderWriterLockSlim ringLock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
    private BloomFilter bloomFilter = new BloomFilter(10000, 5);

    private const int VirtualNodesPerNode = 100;
    private int defaultReplicationFactor = 3;

    // Add a node to the ring
    public void AddNode(string nodeId)
    {
        ringLock.EnterWriteLock();
        try
        {
            if (nodes.ContainsKey(nodeId))
            {
                return;
            }

            CacheNode<TKey, TValue> node = new CacheNode<TKey, TValue>(nodeId);

            // Add virtual nodes
            for (int i = 0; i < VirtualNodesPerNode; i++)
            {
                string virtualId = $"{nodeId}-{i}";
                uint hash = ComputeHash(virtualId);
                node.HashPositions.Add(hash);

                if (!hashRing.ContainsKey(hash))
                {
                    hashRing[hash] = node;
                }
            }

            nodes[nodeId] = node;
        }
        finally
        {
            ringLock.ExitWriteLock();
        }
    }

    // Remove a node from the ring
    public void RemoveNode(string nodeId)
    {
        ringLock.EnterWriteLock();
        try
        {
            if (!nodes.TryRemove(nodeId, out CacheNode<TKey, TValue> node))
            {
                return;
            }

            foreach (uint hash in node.HashPositions)
            {
                hashRing.Remove(hash);
            }
        }
        finally
        {
            ringLock.ExitWriteLock();
        }
    }

    // Set value with replication
    public async Task<bool> SetAsync(TKey key, TValue value, TimeSpan? ttl = null, int replicationFactor = 0)
    {
        if (replicationFactor <= 0)
        {
            replicationFactor = defaultReplicationFactor;
        }

        ringLock.EnterReadLock();
        try
        {
            List<CacheNode<TKey, TValue>> targetNodes = GetNodesForKey(key, replicationFactor);

            if (targetNodes.Count == 0)
            {
                return false;
            }

            CacheEntry<TValue> entry = new CacheEntry<TValue>(value, ttl);
            string keyString = key.ToString();
            bloomFilter.Add(keyString);

            List<Task> tasks = new List<Task>();
            foreach (CacheNode<TKey, TValue> node in targetNodes)
            {
                tasks.Add(Task.Run(() =>
                {
                    entry.NodeId = node.NodeId;
                    node.Data[key] = entry;
                }));
            }

            await Task.WhenAll(tasks);
            return true;
        }
        finally
        {
            ringLock.ExitReadLock();
        }
    }

    // Get value with optional read repair
    public async Task<CacheResult<TValue>> GetAsync(TKey key, bool readRepair = false)
    {
        string keyString = key.ToString();
        if (!bloomFilter.MayContain(keyString))
        {
            return CacheResult<TValue>.Miss();
        }

        ringLock.EnterReadLock();
        try
        {
            List<CacheNode<TKey, TValue>> targetNodes = GetNodesForKey(key, defaultReplicationFactor);

            if (targetNodes.Count == 0)
            {
                return CacheResult<TValue>.Miss();
            }

            // Read from primary node
            CacheNode<TKey, TValue> primaryNode = targetNodes[0];
            if (primaryNode.Data.TryGetValue(key, out CacheEntry<TValue> entry))
            {
                if (entry.IsExpired)
                {
                    primaryNode.Data.TryRemove(key, out _);
                    return CacheResult<TValue>.Miss();
                }

                CacheResult<TValue> result = CacheResult<TValue>.Hit(entry.Value, primaryNode.NodeId, entry.Version);

                // Read repair if enabled
                if (readRepair && targetNodes.Count > 1)
                {
                    await RepairInconsistenciesAsync(key, entry, targetNodes);
                    result.WasRepaired = true;
                }

                return result;
            }

            // Try other replicas
            foreach (CacheNode<TKey, TValue> node in targetNodes.Skip(1))
            {
                if (node.Data.TryGetValue(key, out entry))
                {
                    if (!entry.IsExpired)
                    {
                        return CacheResult<TValue>.Hit(entry.Value, node.NodeId, entry.Version);
                    }
                }
            }

            return CacheResult<TValue>.Miss();
        }
        finally
        {
            ringLock.ExitReadLock();
        }
    }

    // Delete key from all replicas
    public async Task<bool> DeleteAsync(TKey key)
    {
        ringLock.EnterReadLock();
        try
        {
            List<CacheNode<TKey, TValue>> targetNodes = GetNodesForKey(key, defaultReplicationFactor);
            bool deleted = false;

            List<Task> tasks = new List<Task>();
            foreach (CacheNode<TKey, TValue> node in targetNodes)
            {
                tasks.Add(Task.Run(() =>
                {
                    if (node.Data.TryRemove(key, out _))
                    {
                        deleted = true;
                    }
                }));
            }

            await Task.WhenAll(tasks);
            return deleted;
        }
        finally
        {
            ringLock.ExitReadLock();
        }
    }

    // Repair inconsistencies between replicas
    private async Task RepairInconsistenciesAsync(TKey key, CacheEntry<TValue> sourceEntry, List<CacheNode<TKey, TValue>> nodes)
    {
        List<Task> tasks = new List<Task>();

        foreach (CacheNode<TKey, TValue> node in nodes.Skip(1))
        {
            tasks.Add(Task.Run(() =>
            {
                if (!node.Data.TryGetValue(key, out CacheEntry<TValue> existing) || existing.Version < sourceEntry.Version)
                {
                    CacheEntry<TValue> copy = new CacheEntry<TValue>(sourceEntry.Value, null)
                    {
                        Version = sourceEntry.Version,
                        CreatedAt = sourceEntry.CreatedAt,
                        ExpiresAt = sourceEntry.ExpiresAt,
                        NodeId = node.NodeId
                    };
                    node.Data[key] = copy;
                }
            }));
        }

        await Task.WhenAll(tasks);
    }

    // Get nodes responsible for a key
    private List<CacheNode<TKey, TValue>> GetNodesForKey(TKey key, int count)
    {
        uint keyHash = ComputeHash(key.ToString());
        List<CacheNode<TKey, TValue>> result = new List<CacheNode<TKey, TValue>>();
        HashSet<string> addedNodes = new HashSet<string>();

        // Find the first node clockwise from the key hash
        foreach (var kvp in hashRing)
        {
            if (kvp.Key >= keyHash && !addedNodes.Contains(kvp.Value.NodeId))
            {
                result.Add(kvp.Value);
                addedNodes.Add(kvp.Value.NodeId);
                if (result.Count >= count)
                {
                    break;
                }
            }
        }

        // Wrap around if needed
        if (result.Count < count)
        {
            foreach (var kvp in hashRing)
            {
                if (!addedNodes.Contains(kvp.Value.NodeId))
                {
                    result.Add(kvp.Value);
                    addedNodes.Add(kvp.Value.NodeId);
                    if (result.Count >= count)
                    {
                        break;
                    }
                }
            }
        }

        return result;
    }

    // Compute hash for consistent hashing
    private uint ComputeHash(string key)
    {
        using (MD5 md5 = MD5.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(key);
            byte[] hash = md5.ComputeHash(bytes);
            return BitConverter.ToUInt32(hash, 0);
        }
    }

    // Rebalance ring when adding/removing nodes
    public async Task RebalanceRingAsync()
    {
        ringLock.EnterWriteLock();
        try
        {
            List<CacheNode<TKey, TValue>> nodeList = nodes.Values.ToList();
            if (nodeList.Count < 2)
            {
                return;
            }

            // Collect all keys and redistribute
            Dictionary<TKey, CacheEntry<TValue>> allData = new Dictionary<TKey, CacheEntry<TValue>>();
            foreach (CacheNode<TKey, TValue> node in nodeList)
            {
                foreach (var kvp in node.Data)
                {
                    if (!allData.ContainsKey(kvp.Key) || kvp.Value.Version > allData[kvp.Key].Version)
                    {
                        allData[kvp.Key] = kvp.Value;
                    }
                }
                node.Data.Clear();
            }

            // Redistribute to correct nodes
            foreach (var kvp in allData)
            {
                List<CacheNode<TKey, TValue>> targetNodes = GetNodesForKey(kvp.Key, defaultReplicationFactor);
                foreach (CacheNode<TKey, TValue> node in targetNodes)
                {
                    CacheEntry<TValue> entry = new CacheEntry<TValue>(kvp.Value.Value, null)
                    {
                        Version = kvp.Value.Version,
                        CreatedAt = kvp.Value.CreatedAt,
                        ExpiresAt = kvp.Value.ExpiresAt,
                        NodeId = node.NodeId
                    };
                    node.Data[kvp.Key] = entry;
                }
            }
        }
        finally
        {
            ringLock.ExitWriteLock();
        }
    }

    // Query across all nodes
    public async IAsyncEnumerable<KeyValuePair<TKey, TValue>> QueryAsync(
        Func<KeyValuePair<TKey, TValue>, bool> predicate,
        CancellationToken cancellationToken = default)
    {
        HashSet<TKey> seenKeys = new HashSet<TKey>();

        foreach (CacheNode<TKey, TValue> node in nodes.Values)
        {
            foreach (var kvp in node.Data)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    yield break;
                }

                if (seenKeys.Contains(kvp.Key))
                {
                    continue;
                }

                if (!kvp.Value.IsExpired && predicate(new KeyValuePair<TKey, TValue>(kvp.Key, kvp.Value.Value)))
                {
                    seenKeys.Add(kvp.Key);
                    yield return new KeyValuePair<TKey, TValue>(kvp.Key, kvp.Value.Value);
                }
            }
        }
    }

    // Get cache statistics
    public CacheStatistics GetStatistics()
    {
        CacheStatistics stats = new CacheStatistics();
        stats.NodeCount = nodes.Count;

        foreach (CacheNode<TKey, TValue> node in nodes.Values)
        {
            stats.TotalItems += node.ItemCount;
            if (stats.NodeItemCounts == null)
            {
                stats.NodeItemCounts = new Dictionary<string, int>();
            }
            stats.NodeItemCounts[node.NodeId] = node.ItemCount;
        }

        stats.RingSize = hashRing.Count;
        return stats;
    }

    public void Dispose()
    {
        ringLock?.Dispose();
    }
}

// Cache statistics
public class CacheStatistics
{
    public int NodeCount { get; set; }
    public int TotalItems { get; set; }
    public int RingSize { get; set; }
    public Dictionary<string, int> NodeItemCounts { get; set; }
}

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("=== Distributed Cache with Consistent Hashing ===\n");

        using (DistributedCache<string, string> cache = new DistributedCache<string, string>())
        {
            // Add nodes to the cluster
            Console.WriteLine("--- Adding Cache Nodes ---");
            cache.AddNode("node-1");
            cache.AddNode("node-2");
            cache.AddNode("node-3");

            CacheStatistics initialStats = cache.GetStatistics();
            Console.WriteLine($"Nodes in cluster: {initialStats.NodeCount}");
            Console.WriteLine($"Ring positions: {initialStats.RingSize}");

            // Store values
            Console.WriteLine("\n--- Storing Values ---");

            await cache.SetAsync("user:1001", "John Doe", TimeSpan.FromMinutes(30));
            await cache.SetAsync("user:1002", "Jane Smith", TimeSpan.FromMinutes(30));
            await cache.SetAsync("user:1003", "Bob Wilson", TimeSpan.FromMinutes(30));
            await cache.SetAsync("session:abc123", "Active Session Data");
            await cache.SetAsync("config:app", "Application Configuration");

            Console.WriteLine("Stored 5 key-value pairs");

            // Retrieve values
            Console.WriteLine("\n--- Retrieving Values ---");

            CacheResult<string> result1 = await cache.GetAsync("user:1001");
            Console.WriteLine($"user:1001: {(result1.Found ? result1.Value : "Not Found")} (from {result1.FromNode})");

            CacheResult<string> result2 = await cache.GetAsync("user:1002", readRepair: true);
            Console.WriteLine($"user:1002: {(result2.Found ? result2.Value : "Not Found")} (repaired: {result2.WasRepaired})");

            CacheResult<string> result3 = await cache.GetAsync("nonexistent");
            Console.WriteLine($"nonexistent: {(result3.Found ? result3.Value : "Not Found")}");

            // Check statistics
            Console.WriteLine("\n--- Cache Statistics ---");
            CacheStatistics stats = cache.GetStatistics();
            Console.WriteLine($"Total items: {stats.TotalItems}");
            Console.WriteLine("Items per node:");
            foreach (var kvp in stats.NodeItemCounts)
            {
                Console.WriteLine($"  {kvp.Key}: {kvp.Value} items");
            }

            // Add more data
            Console.WriteLine("\n--- Adding More Data ---");
            for (int i = 1; i <= 20; i++)
            {
                await cache.SetAsync($"product:{i}", $"Product {i} Description");
            }
            Console.WriteLine("Added 20 product entries");

            stats = cache.GetStatistics();
            Console.WriteLine("\nUpdated statistics:");
            Console.WriteLine($"Total items: {stats.TotalItems}");
            foreach (var kvp in stats.NodeItemCounts)
            {
                Console.WriteLine($"  {kvp.Key}: {kvp.Value} items");
            }

            // Add a new node and rebalance
            Console.WriteLine("\n--- Adding New Node and Rebalancing ---");
            cache.AddNode("node-4");
            Console.WriteLine("Added node-4");

            await cache.RebalanceRingAsync();
            Console.WriteLine("Rebalancing complete");

            stats = cache.GetStatistics();
            Console.WriteLine("\nAfter rebalancing:");
            foreach (var kvp in stats.NodeItemCounts)
            {
                Console.WriteLine($"  {kvp.Key}: {kvp.Value} items");
            }

            // Query functionality
            Console.WriteLine("\n--- Querying Cache ---");
            Console.WriteLine("Products containing 'Product 1':");
            await foreach (var item in cache.QueryAsync(kvp => kvp.Key.StartsWith("product:1")))
            {
                Console.WriteLine($"  {item.Key}: {item.Value}");
            }

            // Delete a value
            Console.WriteLine("\n--- Deleting Value ---");
            bool deleted = await cache.DeleteAsync("user:1001");
            Console.WriteLine($"Deleted user:1001: {deleted}");

            CacheResult<string> deletedResult = await cache.GetAsync("user:1001");
            Console.WriteLine($"user:1001 after delete: {(deletedResult.Found ? "Found" : "Not Found")}");

            // Remove a node
            Console.WriteLine("\n--- Removing Node ---");
            cache.RemoveNode("node-2");
            Console.WriteLine("Removed node-2");

            stats = cache.GetStatistics();
            Console.WriteLine($"Remaining nodes: {stats.NodeCount}");

            // Verify data is still accessible
            Console.WriteLine("\n--- Verifying Data Access ---");
            CacheResult<string> verifyResult = await cache.GetAsync("user:1002");
            Console.WriteLine($"user:1002: {(verifyResult.Found ? verifyResult.Value : "Not Found")}");

            verifyResult = await cache.GetAsync("product:5");
            Console.WriteLine($"product:5: {(verifyResult.Found ? verifyResult.Value : "Not Found")}");
        }

        Console.WriteLine("\nCache disposed successfully.");
    }
}

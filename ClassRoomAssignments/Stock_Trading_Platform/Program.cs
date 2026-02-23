using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

// Order side enum
public enum OrderSide
{
    Buy,
    Sell
}

// Order status enum
public enum OrderStatus
{
    Pending,
    PartiallyFilled,
    Filled,
    Cancelled
}

// Stock instrument class
public class Stock : IComparable<Stock>
{
    public string Symbol { get; set; }
    public string Name { get; set; }

    public int CompareTo(Stock other)
    {
        return Symbol.CompareTo(other.Symbol);
    }

    public override bool Equals(object obj)
    {
        if (obj is Stock other)
        {
            return Symbol == other.Symbol;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return Symbol.GetHashCode();
    }
}

// Order interface
public interface IOrder<T> where T : IComparable<T>
{
    string OrderId { get; }
    T Instrument { get; }
    OrderSide Side { get; }
    decimal Price { get; }
    int Quantity { get; set; }
    int FilledQuantity { get; set; }
    DateTime Timestamp { get; }
    int Priority { get; }
    OrderStatus Status { get; set; }
}

// Stock order implementation
public class StockOrder : IOrder<Stock>
{
    public string OrderId { get; set; }
    public Stock Instrument { get; set; }
    public OrderSide Side { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public int FilledQuantity { get; set; }
    public DateTime Timestamp { get; set; }
    public int Priority { get; set; }
    public OrderStatus Status { get; set; }

    public int RemainingQuantity => Quantity - FilledQuantity;

    public StockOrder()
    {
        OrderId = Guid.NewGuid().ToString().Substring(0, 8);
        Timestamp = DateTime.Now;
        Status = OrderStatus.Pending;
    }

    public override string ToString()
    {
        return $"[{OrderId}] {Side} {Instrument.Symbol} {Quantity}@{Price:F2} (Priority: {Priority})";
    }
}

// Order match result
public class OrderMatch<T> where T : IComparable<T>
{
    public IOrder<T> BuyOrder { get; set; }
    public IOrder<T> SellOrder { get; set; }
    public decimal MatchPrice { get; set; }
    public int MatchQuantity { get; set; }
    public DateTime MatchTime { get; set; }

    public override string ToString()
    {
        return $"Match: {MatchQuantity} @ {MatchPrice:F2} (Buy: {BuyOrder.OrderId}, Sell: {SellOrder.OrderId})";
    }
}

// Market data class
public class MarketData<T> where T : IComparable<T>
{
    public T Instrument { get; set; }
    public decimal LastPrice { get; set; }
    public decimal BidPrice { get; set; }
    public decimal AskPrice { get; set; }
    public int Volume { get; set; }
    public DateTime Timestamp { get; set; }
}

// Circular buffer for price history
public class CircularBuffer<T>
{
    private T[] buffer;
    private int head;
    private int tail;
    private int count;
    private int capacity;
    private object lockObject = new object();

    public CircularBuffer(int capacity)
    {
        this.capacity = capacity;
        buffer = new T[capacity];
        head = 0;
        tail = 0;
        count = 0;
    }

    public void Add(T item)
    {
        lock (lockObject)
        {
            buffer[head] = item;
            head = (head + 1) % capacity;
            if (count < capacity)
            {
                count++;
            }
            else
            {
                tail = (tail + 1) % capacity;
            }
        }
    }

    public List<T> GetAll()
    {
        lock (lockObject)
        {
            List<T> result = new List<T>();
            int index = tail;
            for (int i = 0; i < count; i++)
            {
                result.Add(buffer[index]);
                index = (index + 1) % capacity;
            }
            return result;
        }
    }

    public int Count
    {
        get
        {
            lock (lockObject)
            {
                return count;
            }
        }
    }
}

// Priority queue for orders
public class OrderPriorityQueue<T> where T : IComparable<T>
{
    private List<IOrder<T>> heap = new List<IOrder<T>>();
    private Comparer<IOrder<T>> comparer;
    private object lockObject = new object();

    public OrderPriorityQueue(Comparer<IOrder<T>> comparer)
    {
        this.comparer = comparer;
    }

    public void Enqueue(IOrder<T> order)
    {
        lock (lockObject)
        {
            heap.Add(order);
            BubbleUp(heap.Count - 1);
        }
    }

    public IOrder<T> Dequeue()
    {
        lock (lockObject)
        {
            if (heap.Count == 0)
            {
                return null;
            }

            IOrder<T> result = heap[0];
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);

            if (heap.Count > 0)
            {
                BubbleDown(0);
            }

            return result;
        }
    }

    public IOrder<T> Peek()
    {
        lock (lockObject)
        {
            if (heap.Count == 0)
            {
                return null;
            }
            return heap[0];
        }
    }

    public int Count
    {
        get
        {
            lock (lockObject)
            {
                return heap.Count;
            }
        }
    }

    public bool Remove(IOrder<T> order)
    {
        lock (lockObject)
        {
            int index = heap.FindIndex(o => o.OrderId == order.OrderId);
            if (index < 0)
            {
                return false;
            }

            heap[index] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);

            if (index < heap.Count)
            {
                BubbleUp(index);
                BubbleDown(index);
            }

            return true;
        }
    }

    private void BubbleUp(int index)
    {
        while (index > 0)
        {
            int parent = (index - 1) / 2;
            if (comparer.Compare(heap[index], heap[parent]) < 0)
            {
                Swap(index, parent);
                index = parent;
            }
            else
            {
                break;
            }
        }
    }

    private void BubbleDown(int index)
    {
        while (true)
        {
            int smallest = index;
            int left = 2 * index + 1;
            int right = 2 * index + 2;

            if (left < heap.Count && comparer.Compare(heap[left], heap[smallest]) < 0)
            {
                smallest = left;
            }

            if (right < heap.Count && comparer.Compare(heap[right], heap[smallest]) < 0)
            {
                smallest = right;
            }

            if (smallest != index)
            {
                Swap(index, smallest);
                index = smallest;
            }
            else
            {
                break;
            }
        }
    }

    private void Swap(int i, int j)
    {
        IOrder<T> temp = heap[i];
        heap[i] = heap[j];
        heap[j] = temp;
    }
}

// Order Book class
public class OrderBook<T> where T : IComparable<T>
{
    private ConcurrentDictionary<string, IOrder<T>> allOrders = new ConcurrentDictionary<string, IOrder<T>>();

    private OrderPriorityQueue<T> buyOrders;
    private OrderPriorityQueue<T> sellOrders;

    private CircularBuffer<decimal> priceHistory = new CircularBuffer<decimal>(1000);
    private ConcurrentDictionary<string, decimal> volumeBySymbol = new ConcurrentDictionary<string, decimal>();
    private List<OrderMatch<T>> matchHistory = new List<OrderMatch<T>>();
    private object matchLock = new object();

    private long orderCount = 0;

    public OrderBook()
    {
        // Buy orders: higher price first, then earlier time
        buyOrders = new OrderPriorityQueue<T>(Comparer<IOrder<T>>.Create((a, b) =>
        {
            int priorityCompare = a.Priority.CompareTo(b.Priority);
            if (priorityCompare != 0) return priorityCompare;

            int priceCompare = b.Price.CompareTo(a.Price);
            if (priceCompare != 0) return priceCompare;

            return a.Timestamp.CompareTo(b.Timestamp);
        }));

        // Sell orders: lower price first, then earlier time
        sellOrders = new OrderPriorityQueue<T>(Comparer<IOrder<T>>.Create((a, b) =>
        {
            int priorityCompare = a.Priority.CompareTo(b.Priority);
            if (priorityCompare != 0) return priorityCompare;

            int priceCompare = a.Price.CompareTo(b.Price);
            if (priceCompare != 0) return priceCompare;

            return a.Timestamp.CompareTo(b.Timestamp);
        }));
    }

    // Process order
    public List<OrderMatch<T>> ProcessOrder(IOrder<T> order)
    {
        Interlocked.Increment(ref orderCount);
        allOrders[order.OrderId] = order;

        List<OrderMatch<T>> matches = new List<OrderMatch<T>>();

        if (order.Side == OrderSide.Buy)
        {
            matches = MatchBuyOrder(order);
            if (order.RemainingQuantity > 0 && order.Status != OrderStatus.Cancelled)
            {
                buyOrders.Enqueue(order);
            }
        }
        else
        {
            matches = MatchSellOrder(order);
            if (order.RemainingQuantity > 0 && order.Status != OrderStatus.Cancelled)
            {
                sellOrders.Enqueue(order);
            }
        }

        return matches;
    }

    private List<OrderMatch<T>> MatchBuyOrder(IOrder<T> buyOrder)
    {
        List<OrderMatch<T>> matches = new List<OrderMatch<T>>();

        while (buyOrder.RemainingQuantity > 0)
        {
            IOrder<T> sellOrder = sellOrders.Peek();
            if (sellOrder == null || sellOrder.Price > buyOrder.Price)
            {
                break;
            }

            sellOrders.Dequeue();

            int matchQty = Math.Min(buyOrder.RemainingQuantity, sellOrder.RemainingQuantity);
            decimal matchPrice = sellOrder.Price;

            buyOrder.FilledQuantity += matchQty;
            sellOrder.FilledQuantity += matchQty;

            // Update statuses
            if (buyOrder.FilledQuantity == buyOrder.Quantity)
            {
                buyOrder.Status = OrderStatus.Filled;
            }
            else
            {
                buyOrder.Status = OrderStatus.PartiallyFilled;
            }

            if (sellOrder.FilledQuantity == sellOrder.Quantity)
            {
                sellOrder.Status = OrderStatus.Filled;
            }
            else
            {
                sellOrder.Status = OrderStatus.PartiallyFilled;
            }

            // Record match
            OrderMatch<T> match = new OrderMatch<T>
            {
                BuyOrder = buyOrder,
                SellOrder = sellOrder,
                MatchPrice = matchPrice,
                MatchQuantity = matchQty,
                MatchTime = DateTime.Now
            };

            matches.Add(match);
            lock (matchLock)
            {
                matchHistory.Add(match);
            }

            priceHistory.Add(matchPrice);

            // Re-add sell order if partially filled
            if (sellOrder.RemainingQuantity > 0)
            {
                sellOrders.Enqueue(sellOrder);
            }
        }

        return matches;
    }

    private List<OrderMatch<T>> MatchSellOrder(IOrder<T> sellOrder)
    {
        List<OrderMatch<T>> matches = new List<OrderMatch<T>>();

        while (sellOrder.RemainingQuantity > 0)
        {
            IOrder<T> buyOrder = buyOrders.Peek();
            if (buyOrder == null || buyOrder.Price < sellOrder.Price)
            {
                break;
            }

            buyOrders.Dequeue();

            int matchQty = Math.Min(sellOrder.RemainingQuantity, buyOrder.RemainingQuantity);
            decimal matchPrice = buyOrder.Price;

            sellOrder.FilledQuantity += matchQty;
            buyOrder.FilledQuantity += matchQty;

            // Update statuses
            if (sellOrder.FilledQuantity == sellOrder.Quantity)
            {
                sellOrder.Status = OrderStatus.Filled;
            }
            else
            {
                sellOrder.Status = OrderStatus.PartiallyFilled;
            }

            if (buyOrder.FilledQuantity == buyOrder.Quantity)
            {
                buyOrder.Status = OrderStatus.Filled;
            }
            else
            {
                buyOrder.Status = OrderStatus.PartiallyFilled;
            }

            // Record match
            OrderMatch<T> match = new OrderMatch<T>
            {
                BuyOrder = buyOrder,
                SellOrder = sellOrder,
                MatchPrice = matchPrice,
                MatchQuantity = matchQty,
                MatchTime = DateTime.Now
            };

            matches.Add(match);
            lock (matchLock)
            {
                matchHistory.Add(match);
            }

            priceHistory.Add(matchPrice);

            // Re-add buy order if partially filled
            if (buyOrder.RemainingQuantity > 0)
            {
                buyOrders.Enqueue(buyOrder);
            }
        }

        return matches;
    }

    // Cancel order
    public bool CancelOrder(string orderId)
    {
        if (allOrders.TryGetValue(orderId, out IOrder<T> order))
        {
            order.Status = OrderStatus.Cancelled;
            if (order.Side == OrderSide.Buy)
            {
                buyOrders.Remove(order);
            }
            else
            {
                sellOrders.Remove(order);
            }
            return true;
        }
        return false;
    }

    // Get order matches
    public IEnumerable<OrderMatch<T>> GetOrderMatches(int count)
    {
        lock (matchLock)
        {
            return matchHistory
                .AsParallel()
                .OrderByDescending(m => m.MatchTime)
                .Take(count)
                .ToList();
        }
    }

    // Calculate VWAP (Volume Weighted Average Price)
    public decimal CalculateVWAP()
    {
        lock (matchLock)
        {
            if (matchHistory.Count == 0)
            {
                return 0;
            }

            decimal totalValue = matchHistory.Sum(m => m.MatchPrice * m.MatchQuantity);
            int totalVolume = matchHistory.Sum(m => m.MatchQuantity);

            if (totalVolume == 0)
            {
                return 0;
            }

            return totalValue / totalVolume;
        }
    }

    // Get price history
    public List<decimal> GetPriceHistory()
    {
        return priceHistory.GetAll();
    }

    // Get order count
    public long GetOrderCount()
    {
        return Interlocked.Read(ref orderCount);
    }

    // Get buy orders count
    public int BuyOrdersCount => buyOrders.Count;

    // Get sell orders count
    public int SellOrdersCount => sellOrders.Count;

    // Get best bid
    public decimal? GetBestBid()
    {
        IOrder<T> order = buyOrders.Peek();
        return order?.Price;
    }

    // Get best ask
    public decimal? GetBestAsk()
    {
        IOrder<T> order = sellOrders.Peek();
        return order?.Price;
    }

    // Get spread
    public decimal? GetSpread()
    {
        decimal? bid = GetBestBid();
        decimal? ask = GetBestAsk();
        if (bid.HasValue && ask.HasValue)
        {
            return ask.Value - bid.Value;
        }
        return null;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Real-Time Stock Trading Platform ===\n");

        OrderBook<Stock> orderBook = new OrderBook<Stock>();

        Stock apple = new Stock { Symbol = "AAPL", Name = "Apple Inc." };

        Console.WriteLine("--- Processing Orders ---\n");

        // Add sell orders
        StockOrder sell1 = new StockOrder { Instrument = apple, Side = OrderSide.Sell, Price = 150.00m, Quantity = 100, Priority = 1 };
        StockOrder sell2 = new StockOrder { Instrument = apple, Side = OrderSide.Sell, Price = 150.50m, Quantity = 200, Priority = 2 };
        StockOrder sell3 = new StockOrder { Instrument = apple, Side = OrderSide.Sell, Price = 151.00m, Quantity = 150, Priority = 1 };

        Console.WriteLine($"Adding: {sell1}");
        orderBook.ProcessOrder(sell1);
        Console.WriteLine($"Adding: {sell2}");
        orderBook.ProcessOrder(sell2);
        Console.WriteLine($"Adding: {sell3}");
        orderBook.ProcessOrder(sell3);

        Console.WriteLine($"\nSell orders in book: {orderBook.SellOrdersCount}");
        Console.WriteLine($"Best Ask: ${orderBook.GetBestAsk():F2}");

        // Add buy orders
        StockOrder buy1 = new StockOrder { Instrument = apple, Side = OrderSide.Buy, Price = 149.50m, Quantity = 50, Priority = 2 };
        StockOrder buy2 = new StockOrder { Instrument = apple, Side = OrderSide.Buy, Price = 149.00m, Quantity = 100, Priority = 1 };

        Console.WriteLine($"\nAdding: {buy1}");
        orderBook.ProcessOrder(buy1);
        Console.WriteLine($"Adding: {buy2}");
        orderBook.ProcessOrder(buy2);

        Console.WriteLine($"\nBuy orders in book: {orderBook.BuyOrdersCount}");
        Console.WriteLine($"Best Bid: ${orderBook.GetBestBid():F2}");
        Console.WriteLine($"Spread: ${orderBook.GetSpread():F2}");

        // Process matching order
        Console.WriteLine("\n--- Matching Order ---");
        StockOrder buyMatch = new StockOrder { Instrument = apple, Side = OrderSide.Buy, Price = 150.50m, Quantity = 250, Priority = 1 };
        Console.WriteLine($"Processing: {buyMatch}");

        List<OrderMatch<Stock>> matches = orderBook.ProcessOrder(buyMatch);

        Console.WriteLine($"\nMatches executed: {matches.Count}");
        foreach (OrderMatch<Stock> match in matches)
        {
            Console.WriteLine($"  {match}");
        }

        Console.WriteLine($"\nBuy order status: {buyMatch.Status}");
        Console.WriteLine($"Buy order filled: {buyMatch.FilledQuantity}/{buyMatch.Quantity}");

        // Market statistics
        Console.WriteLine("\n--- Market Statistics ---");
        Console.WriteLine($"Total orders processed: {orderBook.GetOrderCount()}");
        Console.WriteLine($"VWAP: ${orderBook.CalculateVWAP():F2}");
        Console.WriteLine($"Buy orders in book: {orderBook.BuyOrdersCount}");
        Console.WriteLine($"Sell orders in book: {orderBook.SellOrdersCount}");

        // Price history
        Console.WriteLine("\n--- Price History ---");
        List<decimal> prices = orderBook.GetPriceHistory();
        foreach (decimal price in prices)
        {
            Console.WriteLine($"  ${price:F2}");
        }

        // More complex scenario
        Console.WriteLine("\n--- High Volume Trading Simulation ---");
        Random random = new Random(42);

        for (int i = 0; i < 20; i++)
        {
            decimal price = 149.00m + (decimal)(random.NextDouble() * 3);
            int quantity = random.Next(10, 100);
            OrderSide side = random.Next(2) == 0 ? OrderSide.Buy : OrderSide.Sell;
            int priority = random.Next(1, 4);

            StockOrder order = new StockOrder
            {
                Instrument = apple,
                Side = side,
                Price = Math.Round(price, 2),
                Quantity = quantity,
                Priority = priority
            };

            List<OrderMatch<Stock>> orderMatches = orderBook.ProcessOrder(order);
            if (orderMatches.Count > 0)
            {
                Console.WriteLine($"Order {order.OrderId} ({side}) matched {orderMatches.Count} times");
            }
        }

        Console.WriteLine("\n--- Final Statistics ---");
        Console.WriteLine($"Total orders processed: {orderBook.GetOrderCount()}");
        Console.WriteLine($"Total matches: {orderBook.GetOrderMatches(1000).Count()}");
        Console.WriteLine($"VWAP: ${orderBook.CalculateVWAP():F2}");
        Console.WriteLine($"Buy orders remaining: {orderBook.BuyOrdersCount}");
        Console.WriteLine($"Sell orders remaining: {orderBook.SellOrdersCount}");

        // Recent matches
        Console.WriteLine("\n--- Recent 5 Matches ---");
        IEnumerable<OrderMatch<Stock>> recentMatches = orderBook.GetOrderMatches(5);
        foreach (OrderMatch<Stock> match in recentMatches)
        {
            Console.WriteLine($"  {match}");
        }
    }
}

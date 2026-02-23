using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

// Time series point
public class TimeSeriesPoint<TTimestamp, TValue>
    where TTimestamp : IComparable<TTimestamp>, IEquatable<TTimestamp>
    where TValue : struct
{
    public TTimestamp Timestamp { get; set; }
    public TValue Value { get; set; }
    public bool IsNull { get; set; }

    public TimeSeriesPoint(TTimestamp timestamp, TValue value, bool isNull = false)
    {
        Timestamp = timestamp;
        Value = value;
        IsNull = isNull;
    }
}

// Window result
public class WindowResult<TTimestamp, TAggregate>
{
    public TTimestamp WindowStart { get; set; }
    public TTimestamp WindowEnd { get; set; }
    public TAggregate AggregateValue { get; set; }
    public int PointCount { get; set; }
}

// Pattern match
public class PatternMatch<TTimestamp>
{
    public TTimestamp StartTime { get; set; }
    public TTimestamp EndTime { get; set; }
    public double Similarity { get; set; }
    public int StartIndex { get; set; }
    public int EndIndex { get; set; }
}

// Window alignment
public enum WindowAlignment
{
    Left,
    Center,
    Right
}

// Correlation method
public enum CorrelationMethod
{
    Pearson,
    Spearman
}

// Correlation matrix
public class CorrelationMatrix
{
    private Dictionary<string, Dictionary<string, double>> matrix = new Dictionary<string, Dictionary<string, double>>();

    public void SetCorrelation(string series1, string series2, double value)
    {
        if (!matrix.ContainsKey(series1))
        {
            matrix[series1] = new Dictionary<string, double>();
        }
        matrix[series1][series2] = value;
    }

    public double GetCorrelation(string series1, string series2)
    {
        if (matrix.ContainsKey(series1) && matrix[series1].ContainsKey(series2))
        {
            return matrix[series1][series2];
        }
        return 0;
    }

    public IEnumerable<string> GetSeriesNames()
    {
        return matrix.Keys;
    }
}

// Segmented list for columnar storage
public class SegmentedList<T>
{
    private List<T[]> segments = new List<T[]>();
    private int segmentSize;
    private int count;
    private object lockObject = new object();

    public SegmentedList(int segmentSize = 10000)
    {
        this.segmentSize = segmentSize;
        count = 0;
    }

    public void Add(T item)
    {
        lock (lockObject)
        {
            int segmentIndex = count / segmentSize;
            int indexInSegment = count % segmentSize;

            while (segments.Count <= segmentIndex)
            {
                segments.Add(new T[segmentSize]);
            }

            segments[segmentIndex][indexInSegment] = item;
            count++;
        }
    }

    public T this[int index]
    {
        get
        {
            lock (lockObject)
            {
                if (index < 0 || index >= count)
                {
                    throw new IndexOutOfRangeException();
                }

                int segmentIndex = index / segmentSize;
                int indexInSegment = index % segmentSize;
                return segments[segmentIndex][indexInSegment];
            }
        }
        set
        {
            lock (lockObject)
            {
                if (index < 0 || index >= count)
                {
                    throw new IndexOutOfRangeException();
                }

                int segmentIndex = index / segmentSize;
                int indexInSegment = index % segmentSize;
                segments[segmentIndex][indexInSegment] = value;
            }
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

    public void Clear()
    {
        lock (lockObject)
        {
            segments.Clear();
            count = 0;
        }
    }
}

// Interval tree node for time range queries
public class IntervalTreeNode<TTimestamp, TData>
    where TTimestamp : IComparable<TTimestamp>
{
    public TTimestamp Start { get; set; }
    public TTimestamp End { get; set; }
    public TData Data { get; set; }
    public TTimestamp MaxEnd { get; set; }
    public IntervalTreeNode<TTimestamp, TData> Left { get; set; }
    public IntervalTreeNode<TTimestamp, TData> Right { get; set; }
}

// Interval tree
public class IntervalTree<TTimestamp, TData>
    where TTimestamp : IComparable<TTimestamp>
{
    private IntervalTreeNode<TTimestamp, TData> root;

    public void Insert(TTimestamp start, TTimestamp end, TData data)
    {
        root = InsertNode(root, start, end, data);
    }

    private IntervalTreeNode<TTimestamp, TData> InsertNode(IntervalTreeNode<TTimestamp, TData> node,
        TTimestamp start, TTimestamp end, TData data)
    {
        if (node == null)
        {
            return new IntervalTreeNode<TTimestamp, TData>
            {
                Start = start,
                End = end,
                Data = data,
                MaxEnd = end
            };
        }

        if (start.CompareTo(node.Start) < 0)
        {
            node.Left = InsertNode(node.Left, start, end, data);
        }
        else
        {
            node.Right = InsertNode(node.Right, start, end, data);
        }

        if (end.CompareTo(node.MaxEnd) > 0)
        {
            node.MaxEnd = end;
        }

        return node;
    }

    public List<TData> Query(TTimestamp point)
    {
        List<TData> results = new List<TData>();
        QueryNode(root, point, results);
        return results;
    }

    private void QueryNode(IntervalTreeNode<TTimestamp, TData> node, TTimestamp point, List<TData> results)
    {
        if (node == null)
        {
            return;
        }

        if (point.CompareTo(node.Start) >= 0 && point.CompareTo(node.End) <= 0)
        {
            results.Add(node.Data);
        }

        if (node.Left != null && point.CompareTo(node.Left.MaxEnd) <= 0)
        {
            QueryNode(node.Left, point, results);
        }

        QueryNode(node.Right, point, results);
    }
}

// Time-Series Database
public class TimeSeriesDatabase<TValue>
    where TValue : struct
{
    private SegmentedList<DateTime> timestamps = new SegmentedList<DateTime>(10000);
    private SegmentedList<TValue> values = new SegmentedList<TValue>(10000);
    private SegmentedList<bool> nullFlags = new SegmentedList<bool>(10000);
    private IntervalTree<DateTime, int> timeIndex = new IntervalTree<DateTime, int>();
    private SortedDictionary<DateTime, int> timestampIndex = new SortedDictionary<DateTime, int>();

    private object appendLock = new object();

    // Append data points
    public void Append(IEnumerable<TimeSeriesPoint<DateTime, TValue>> points)
    {
        lock (appendLock)
        {
            foreach (TimeSeriesPoint<DateTime, TValue> point in points)
            {
                int index = timestamps.Count;

                timestamps.Add(point.Timestamp);
                values.Add(point.Value);
                nullFlags.Add(point.IsNull);

                timestampIndex[point.Timestamp] = index;
            }
        }
    }

    // Append single point
    public void Append(DateTime timestamp, TValue value, bool isNull = false)
    {
        Append(new[] { new TimeSeriesPoint<DateTime, TValue>(timestamp, value, isNull) });
    }

    // Get point count
    public int Count => timestamps.Count;

    // Query by time range
    public List<TimeSeriesPoint<DateTime, TValue>> Query(DateTime start, DateTime end)
    {
        List<TimeSeriesPoint<DateTime, TValue>> results = new List<TimeSeriesPoint<DateTime, TValue>>();

        for (int i = 0; i < timestamps.Count; i++)
        {
            DateTime ts = timestamps[i];
            if (ts >= start && ts <= end)
            {
                results.Add(new TimeSeriesPoint<DateTime, TValue>(ts, values[i], nullFlags[i]));
            }
        }

        return results.OrderBy(p => p.Timestamp).ToList();
    }

    // Get value at specific time
    public TValue? GetValue(DateTime timestamp)
    {
        for (int i = 0; i < timestamps.Count; i++)
        {
            if (timestamps[i].Equals(timestamp))
            {
                if (nullFlags[i])
                {
                    return null;
                }
                return values[i];
            }
        }
        return null;
    }

    // Rolling window aggregation
    public IEnumerable<WindowResult<DateTime, TAggregate>> RollingWindow<TAggregate>(
        DateTime start,
        DateTime end,
        TimeSpan windowSize,
        TimeSpan step,
        Func<IEnumerable<TValue>, TAggregate> aggregator,
        WindowAlignment alignment = WindowAlignment.Left)
    {
        List<WindowResult<DateTime, TAggregate>> results = new List<WindowResult<DateTime, TAggregate>>();

        DateTime windowStart = start;

        while (windowStart < end)
        {
            DateTime windowEnd = windowStart.Add(windowSize);
            if (windowEnd > end)
            {
                windowEnd = end;
            }

            // Adjust for alignment
            DateTime actualStart = windowStart;
            DateTime actualEnd = windowEnd;

            if (alignment == WindowAlignment.Center)
            {
                TimeSpan halfWindow = TimeSpan.FromTicks(windowSize.Ticks / 2);
                actualStart = windowStart.Subtract(halfWindow);
                actualEnd = windowStart.Add(halfWindow);
            }
            else if (alignment == WindowAlignment.Right)
            {
                actualStart = windowStart.Subtract(windowSize);
                actualEnd = windowStart;
            }

            // Get values in window
            List<TValue> windowValues = new List<TValue>();
            for (int i = 0; i < timestamps.Count; i++)
            {
                DateTime ts = timestamps[i];
                if (ts >= actualStart && ts < actualEnd && !nullFlags[i])
                {
                    windowValues.Add(values[i]);
                }
            }

            if (windowValues.Count > 0)
            {
                WindowResult<DateTime, TAggregate> windowResult = new WindowResult<DateTime, TAggregate>
                {
                    WindowStart = actualStart,
                    WindowEnd = actualEnd,
                    AggregateValue = aggregator(windowValues),
                    PointCount = windowValues.Count
                };
                results.Add(windowResult);
            }

            windowStart = windowStart.Add(step);
        }

        return results;
    }

    // Find pattern in time series (simplified DTW-based)
    public IEnumerable<PatternMatch<DateTime>> FindPatterns(
        IEnumerable<TValue> pattern,
        double similarityThreshold = 0.8)
    {
        List<PatternMatch<DateTime>> matches = new List<PatternMatch<DateTime>>();
        List<TValue> patternList = pattern.ToList();
        int patternLength = patternList.Count;

        if (patternLength == 0 || timestamps.Count < patternLength)
        {
            return matches;
        }

        // Sliding window search
        for (int i = 0; i <= timestamps.Count - patternLength; i++)
        {
            List<TValue> windowValues = new List<TValue>();
            for (int j = 0; j < patternLength; j++)
            {
                if (!nullFlags[i + j])
                {
                    windowValues.Add(values[i + j]);
                }
            }

            if (windowValues.Count == patternLength)
            {
                double similarity = CalculateSimilarity(patternList, windowValues);
                if (similarity >= similarityThreshold)
                {
                    matches.Add(new PatternMatch<DateTime>
                    {
                        StartTime = timestamps[i],
                        EndTime = timestamps[i + patternLength - 1],
                        Similarity = similarity,
                        StartIndex = i,
                        EndIndex = i + patternLength - 1
                    });
                }
            }
        }

        return matches;
    }

    // Calculate similarity between two sequences
    private double CalculateSimilarity(List<TValue> seq1, List<TValue> seq2)
    {
        if (seq1.Count != seq2.Count || seq1.Count == 0)
        {
            return 0;
        }

        // Normalize sequences
        List<double> norm1 = NormalizeSequence(seq1);
        List<double> norm2 = NormalizeSequence(seq2);

        // Calculate correlation
        double mean1 = norm1.Average();
        double mean2 = norm2.Average();

        double numerator = 0;
        double denominator1 = 0;
        double denominator2 = 0;

        for (int i = 0; i < norm1.Count; i++)
        {
            double diff1 = norm1[i] - mean1;
            double diff2 = norm2[i] - mean2;
            numerator += diff1 * diff2;
            denominator1 += diff1 * diff1;
            denominator2 += diff2 * diff2;
        }

        double denominator = Math.Sqrt(denominator1 * denominator2);
        if (denominator == 0)
        {
            return 1;
        }

        double correlation = numerator / denominator;
        return (correlation + 1) / 2; // Normalize to [0, 1]
    }

    private List<double> NormalizeSequence(List<TValue> sequence)
    {
        List<double> doubleSeq = sequence.Select(v => Convert.ToDouble(v)).ToList();
        double min = doubleSeq.Min();
        double max = doubleSeq.Max();
        double range = max - min;

        if (range == 0)
        {
            return doubleSeq.Select(v => 0.5).ToList();
        }

        return doubleSeq.Select(v => (v - min) / range).ToList();
    }

    // Calculate statistics for a time range
    public TimeSeriesStatistics<TValue> GetStatistics(DateTime start, DateTime end)
    {
        List<double> rangeValues = new List<double>();

        for (int i = 0; i < timestamps.Count; i++)
        {
            DateTime ts = timestamps[i];
            if (ts >= start && ts <= end && !nullFlags[i])
            {
                rangeValues.Add(Convert.ToDouble(values[i]));
            }
        }

        if (rangeValues.Count == 0)
        {
            return new TimeSeriesStatistics<TValue>();
        }

        TimeSeriesStatistics<TValue> stats = new TimeSeriesStatistics<TValue>
        {
            Count = rangeValues.Count,
            Min = rangeValues.Min(),
            Max = rangeValues.Max(),
            Mean = rangeValues.Average(),
            Sum = rangeValues.Sum()
        };

        // Calculate standard deviation
        double meanVal = stats.Mean;
        double sumSquaredDiff = rangeValues.Sum(v => (v - meanVal) * (v - meanVal));
        stats.StdDev = Math.Sqrt(sumSquaredDiff / rangeValues.Count);

        // Calculate median
        List<double> sorted = rangeValues.OrderBy(v => v).ToList();
        int mid = sorted.Count / 2;
        if (sorted.Count % 2 == 0)
        {
            stats.Median = (sorted[mid - 1] + sorted[mid]) / 2;
        }
        else
        {
            stats.Median = sorted[mid];
        }

        return stats;
    }

    // Cross-correlation between this series and another
    public double CrossCorrelate(TimeSeriesDatabase<TValue> other, int lag = 0)
    {
        int minCount = Math.Min(timestamps.Count, other.Count);
        if (minCount <= Math.Abs(lag))
        {
            return 0;
        }

        List<double> series1 = new List<double>();
        List<double> series2 = new List<double>();

        int start1 = lag >= 0 ? lag : 0;
        int start2 = lag >= 0 ? 0 : -lag;
        int length = minCount - Math.Abs(lag);

        for (int i = 0; i < length; i++)
        {
            if (!nullFlags[start1 + i] && !other.nullFlags[start2 + i])
            {
                series1.Add(Convert.ToDouble(values[start1 + i]));
                series2.Add(Convert.ToDouble(other.values[start2 + i]));
            }
        }

        if (series1.Count < 2)
        {
            return 0;
        }

        // Pearson correlation
        double mean1 = series1.Average();
        double mean2 = series2.Average();

        double numerator = 0;
        double denominator1 = 0;
        double denominator2 = 0;

        for (int i = 0; i < series1.Count; i++)
        {
            double diff1 = series1[i] - mean1;
            double diff2 = series2[i] - mean2;
            numerator += diff1 * diff2;
            denominator1 += diff1 * diff1;
            denominator2 += diff2 * diff2;
        }

        double denominator = Math.Sqrt(denominator1 * denominator2);
        if (denominator == 0)
        {
            return 0;
        }

        return numerator / denominator;
    }

    // Get all values as list
    public List<TimeSeriesPoint<DateTime, TValue>> GetAll()
    {
        List<TimeSeriesPoint<DateTime, TValue>> results = new List<TimeSeriesPoint<DateTime, TValue>>();
        for (int i = 0; i < timestamps.Count; i++)
        {
            results.Add(new TimeSeriesPoint<DateTime, TValue>(timestamps[i], values[i], nullFlags[i]));
        }
        return results;
    }

    // Clear all data
    public void Clear()
    {
        lock (appendLock)
        {
            timestamps.Clear();
            values.Clear();
            nullFlags.Clear();
            timestampIndex.Clear();
        }
    }
}

// Time series statistics
public class TimeSeriesStatistics<TValue> where TValue : struct
{
    public int Count { get; set; }
    public double Min { get; set; }
    public double Max { get; set; }
    public double Mean { get; set; }
    public double Median { get; set; }
    public double StdDev { get; set; }
    public double Sum { get; set; }

    public override string ToString()
    {
        return $"Count: {Count}, Min: {Min:F2}, Max: {Max:F2}, Mean: {Mean:F2}, Median: {Median:F2}, StdDev: {StdDev:F2}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Time-Series Database Engine ===\n");

        TimeSeriesDatabase<double> temperatureDb = new TimeSeriesDatabase<double>();

        // Generate sample data - temperature readings over a day
        Console.WriteLine("--- Generating Temperature Data ---");
        DateTime startTime = new DateTime(2024, 1, 1, 0, 0, 0);
        Random random = new Random(42);

        List<TimeSeriesPoint<DateTime, double>> points = new List<TimeSeriesPoint<DateTime, double>>();

        for (int i = 0; i < 288; i++) // Every 5 minutes for 24 hours
        {
            DateTime timestamp = startTime.AddMinutes(i * 5);

            // Simulate temperature: base temp + daily cycle + noise
            double hour = i * 5.0 / 60.0;
            double baseTemp = 20;
            double dailyCycle = 5 * Math.Sin((hour - 6) * Math.PI / 12); // Peak at noon
            double noise = (random.NextDouble() - 0.5) * 2;

            double temperature = baseTemp + dailyCycle + noise;

            points.Add(new TimeSeriesPoint<DateTime, double>(timestamp, temperature));
        }

        temperatureDb.Append(points);
        Console.WriteLine($"Added {temperatureDb.Count} data points");

        // Query data
        Console.WriteLine("\n--- Querying Data ---");
        DateTime queryStart = startTime.AddHours(6);
        DateTime queryEnd = startTime.AddHours(12);

        List<TimeSeriesPoint<DateTime, double>> morningData = temperatureDb.Query(queryStart, queryEnd);
        Console.WriteLine($"Morning data (6 AM - 12 PM): {morningData.Count} points");
        Console.WriteLine($"First: {morningData.First().Timestamp:HH:mm} = {morningData.First().Value:F2}°C");
        Console.WriteLine($"Last: {morningData.Last().Timestamp:HH:mm} = {morningData.Last().Value:F2}°C");

        // Statistics
        Console.WriteLine("\n--- Statistics ---");
        TimeSeriesStatistics<double> fullStats = temperatureDb.GetStatistics(startTime, startTime.AddDays(1));
        Console.WriteLine($"Full day: {fullStats}");

        TimeSeriesStatistics<double> morningStats = temperatureDb.GetStatistics(queryStart, queryEnd);
        Console.WriteLine($"Morning: {morningStats}");

        // Rolling window - hourly averages
        Console.WriteLine("\n--- Hourly Averages (Rolling Window) ---");
        IEnumerable<WindowResult<DateTime, double>> hourlyAvg = temperatureDb.RollingWindow(
            startTime,
            startTime.AddHours(12),
            TimeSpan.FromHours(1),
            TimeSpan.FromHours(1),
            values => values.Average()
        );

        foreach (WindowResult<DateTime, double> window in hourlyAvg)
        {
            Console.WriteLine($"{window.WindowStart:HH:mm}-{window.WindowEnd:HH:mm}: Avg={window.AggregateValue:F2}°C ({window.PointCount} points)");
        }

        // Moving average with smaller step
        Console.WriteLine("\n--- 15-Minute Moving Average ---");
        IEnumerable<WindowResult<DateTime, double>> movingAvg = temperatureDb.RollingWindow(
            startTime.AddHours(8),
            startTime.AddHours(10),
            TimeSpan.FromMinutes(15),
            TimeSpan.FromMinutes(5),
            values => values.Average()
        );

        foreach (WindowResult<DateTime, double> window in movingAvg.Take(5))
        {
            Console.WriteLine($"{window.WindowStart:HH:mm}: {window.AggregateValue:F2}°C");
        }

        // Pattern matching
        Console.WriteLine("\n--- Pattern Matching ---");
        // Create a pattern representing a temperature rise
        List<double> pattern = new List<double> { 18, 19, 20, 21, 22 };
        IEnumerable<PatternMatch<DateTime>> patternMatches = temperatureDb.FindPatterns(pattern, 0.7);
        Console.WriteLine($"Found {patternMatches.Count()} matches for rising temperature pattern");

        foreach (PatternMatch<DateTime> match in patternMatches.Take(3))
        {
            Console.WriteLine($"  {match.StartTime:HH:mm} - {match.EndTime:HH:mm}: Similarity={match.Similarity:F2}");
        }

        // Create second time series for correlation
        Console.WriteLine("\n--- Cross-Correlation Analysis ---");
        TimeSeriesDatabase<double> humidityDb = new TimeSeriesDatabase<double>();

        List<TimeSeriesPoint<DateTime, double>> humidityPoints = new List<TimeSeriesPoint<DateTime, double>>();
        for (int i = 0; i < 288; i++)
        {
            DateTime timestamp = startTime.AddMinutes(i * 5);

            // Humidity inversely related to temperature with lag
            double hour = i * 5.0 / 60.0;
            double baseHumidity = 60;
            double dailyCycle = -10 * Math.Sin((hour - 8) * Math.PI / 12); // Inverse of temp, 2 hour lag
            double noise = (random.NextDouble() - 0.5) * 5;

            double humidity = baseHumidity + dailyCycle + noise;

            humidityPoints.Add(new TimeSeriesPoint<DateTime, double>(timestamp, humidity));
        }
        humidityDb.Append(humidityPoints);

        double correlation0 = temperatureDb.CrossCorrelate(humidityDb, 0);
        double correlationLag2 = temperatureDb.CrossCorrelate(humidityDb, 24); // 2 hour lag (24 * 5min)
        double correlationLagNeg2 = temperatureDb.CrossCorrelate(humidityDb, -24);

        Console.WriteLine($"Correlation (no lag): {correlation0:F3}");
        Console.WriteLine($"Correlation (2hr lag): {correlationLag2:F3}");
        Console.WriteLine($"Correlation (-2hr lag): {correlationLagNeg2:F3}");

        // Window aggregations with different functions
        Console.WriteLine("\n--- Window Aggregations ---");

        IEnumerable<WindowResult<DateTime, double>> hourlyMin = temperatureDb.RollingWindow(
            startTime.AddHours(10),
            startTime.AddHours(14),
            TimeSpan.FromHours(1),
            TimeSpan.FromHours(1),
            values => values.Min()
        );

        IEnumerable<WindowResult<DateTime, double>> hourlyMax = temperatureDb.RollingWindow(
            startTime.AddHours(10),
            startTime.AddHours(14),
            TimeSpan.FromHours(1),
            TimeSpan.FromHours(1),
            values => values.Max()
        );

        IEnumerable<WindowResult<DateTime, double>> hourlyAvgList = temperatureDb.RollingWindow(
            startTime.AddHours(10),
            startTime.AddHours(14),
            TimeSpan.FromHours(1),
            TimeSpan.FromHours(1),
            values => values.Average()
        );

        Console.WriteLine($"{"Hour",-10}{"Min",-10}{"Max",-10}{"Avg",-10}");
        Console.WriteLine(new string('-', 40));

        var minList = hourlyMin.ToList();
        var maxList = hourlyMax.ToList();
        var avgList = hourlyAvgList.ToList();

        for (int i = 0; i < minList.Count; i++)
        {
            Console.WriteLine($"{minList[i].WindowStart:HH:mm}-{minList[i].WindowEnd:HH:mm}  {minList[i].AggregateValue:F2}     {maxList[i].AggregateValue:F2}     {avgList[i].AggregateValue:F2}");
        }

        Console.WriteLine("\n--- Performance Test ---");
        TimeSeriesDatabase<double> largeDb = new TimeSeriesDatabase<double>();

        DateTime perfStart = DateTime.Now;
        List<TimeSeriesPoint<DateTime, double>> largePoints = new List<TimeSeriesPoint<DateTime, double>>();
        for (int i = 0; i < 100000; i++)
        {
            largePoints.Add(new TimeSeriesPoint<DateTime, double>(
                startTime.AddSeconds(i),
                random.NextDouble() * 100
            ));
        }
        largeDb.Append(largePoints);
        TimeSpan insertTime = DateTime.Now - perfStart;

        Console.WriteLine($"Inserted 100,000 points in {insertTime.TotalMilliseconds:F0}ms");

        perfStart = DateTime.Now;
        TimeSeriesStatistics<double> largeStats = largeDb.GetStatistics(startTime, startTime.AddDays(1));
        TimeSpan statsTime = DateTime.Now - perfStart;

        Console.WriteLine($"Calculated statistics in {statsTime.TotalMilliseconds:F0}ms");
        Console.WriteLine($"Large dataset: {largeStats}");
    }
}

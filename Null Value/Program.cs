class Program
{
    public static double? AverageNonNull(double?[] values) // ? makes nullable
    {
        if (values == null || values.Length == 0)
            return null;

        double sum = 0;
        int count = 0;

        foreach (var v in values)
        {
            if (v.HasValue) // checks whether v has null value or not
            {
                sum += v.Value;
                count++;
            }
        }

        if (count == 0)
            return null;

        double avg = sum / count;
        return Math.Round(avg, 2, MidpointRounding.AwayFromZero);
    }

    public static void Main()
    {
        double?[] array = {12.4,34.34,null,45.3};
        Console.WriteLine("Avg is: "+AverageNonNull(array));
    }
}
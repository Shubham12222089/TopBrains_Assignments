using System;

public class Program
{
    public static int SumOnlyIntegers(object[] values)
    {
        int sum = 0;

        foreach (object item in values)
        {
            if (item is int x) // checking
            {
                sum += x;
            }
        }
        return sum;
    }

    public static void Main()
    {
        int n = Convert.ToInt32(Console.ReadLine());
        object[] values = new object[n];

        for (int i = 0; i < n; i++)
        {
            values[i] = Console.ReadLine(); 
        }
        int result = SumOnlyIntegers(values);
        Console.WriteLine(result);
    }
}

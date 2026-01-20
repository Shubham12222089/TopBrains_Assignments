using System;
using System.Data;
using System.Security.Cryptography;

class Program
{
    static bool IsPrime(int num){
        if (num <= 1)
        {
            return false;
        }
        for(int i = 2; i < Math.Sqrt(num); i++)
        {
            if (num % i == 0)
            {
                return false;
            }
        }
        return true;
    }
    static int SumOfDigits(int n)
    {
        int sum = 0;
        while (n > 0)
        {
            sum += n % 10;
            n /= 10;
        }
        return sum;
    }
    public static void Main(string[] args)
    {
        string[] input = Console.ReadLine().Split();
        int m = int.Parse(input[0]);
        int n = int.Parse(input[1]);
        int count=0;
        for (int x = m; x <= n; x++)
        {
            if (!IsPrime(x))
            {
                int s1 = SumOfDigits(x);
                int s2 = SumOfDigits(x * x);

                if (s2 == s1 * s1)
                {
                    count++;
                }
            }
        }

        Console.WriteLine(count);
    }
}
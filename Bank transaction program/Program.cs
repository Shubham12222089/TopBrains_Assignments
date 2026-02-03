using System;

public class Program
{
    public static int GetFinalBalance(int initialBalance, int[] transactions)
    {
        int balance = initialBalance;

        foreach (int tx in transactions)
        {
            if (tx >= 0)
            {
                balance += tx;
            }
            else
            {
                int withdrawal = -tx;
                if (balance >= withdrawal)
                {
                    balance -= withdrawal; 
                }
            }
        }

        return balance;
    }
    public static void Main()
    {
        int initialBalance = Convert.ToInt32(Console.ReadLine());
        int n = Convert.ToInt32(Console.ReadLine());

        int[] transactions = new int[n];
        for (int i = 0; i < n; i++)
        {
            transactions[i] = Convert.ToInt32(Console.ReadLine());
        }

        int finalBalance = GetFinalBalance(initialBalance, transactions);
        Console.WriteLine(finalBalance);
    }
}

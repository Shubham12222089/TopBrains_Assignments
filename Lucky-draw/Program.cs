
class Program
{
    static void Main()
    {
        int m = int.Parse(Console.ReadLine());
        int n = int.Parse(Console.ReadLine());

        int count = 0;

        for (int x = m; x <= n; x++)
        {
            // prime check
            bool isPrime = true;

            if (x <= 1)
                isPrime = false;
            else
            {
                for (int i = 2; i <= x / 2; i++)
                {
                    if (x % i == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
            }

            if (isPrime && x > 1)
                continue;   // skip prime numbers

            // sum of digits of x
            int temp = x;
            int sumX = 0;
            while (temp > 0)
            {
                sumX += temp % 10;
                temp /= 10;
            }

            // sum of digits of x*x
            int square = x * x;
            int sumSquare = 0;
            while (square > 0)
            {
                sumSquare += square % 10;
                square /= 10;
            }

            if (sumSquare == sumX * sumX)
                count++;
        }

        Console.WriteLine(count);
    }
}

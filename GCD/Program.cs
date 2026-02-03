
public class Program
{
    public static int GCD(int a, int b)
    {
        if (b == 0)
            return a;

        return GCD(b, a % b); // recursion
    }

    public static void Main()
    {
        int a = Convert.ToInt32(Console.ReadLine());
        int b = Convert.ToInt32(Console.ReadLine());

        int result = GCD(a, b);
        Console.WriteLine(result);
    }
}

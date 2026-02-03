
public class Program
{
    public static int[] GetMultiplicationRow(int n, int upto)
    {
        int[] row = new int[upto];

        for (int i = 1; i <= upto; i++)
        {
            row[i - 1] = n * i; // bcz indexing is from 0
        }

        return row;
    }

    public static void Main()
    {
        int n = Convert.ToInt32(Console.ReadLine());
        int upto = Convert.ToInt32(Console.ReadLine());

        int[] result = GetMultiplicationRow(n, upto);

        foreach (int value in result)
        {
            Console.Write(value + " ");
        }
    }
}

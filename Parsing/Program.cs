
public class Program
{
    public static int SumParsedIntegers(string[] tokens)
    {
        int sum = 0;
        
        foreach (string token in tokens)
        {
            if (int.TryParse(token, out int value)) // check krne k liye, chks the range while parsing 
            {
                sum += value;
            }
        }
        return sum;
    }

    public static void Main()
    {
        int n = Convert.ToInt32(Console.ReadLine());
        string[] tokens = new string[n];

        for (int i = 0; i < n; i++)
        {
            tokens[i] = Console.ReadLine();
        }

        int result = SumParsedIntegers(tokens);
        Console.WriteLine(result);
    }
}

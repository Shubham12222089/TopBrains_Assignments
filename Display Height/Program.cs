
public class Program
{
    public static string GetHeightCategory(int height)
    {
        if (height < 150) return "Short";
        else if (height < 180) return "Average";
        else return "Tall";
    }

    public static void Main()
    {
        int heightCm = Convert.ToInt32(Console.ReadLine());
        string category = GetHeightCategory(heightCm);
        Console.WriteLine(category);
    }
}
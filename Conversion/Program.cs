
public class Program
{
    public static double ConvertFeetToCentimeters(int feet)
    {
        double centimeters = feet * 30.48;
        return Math.Round(centimeters, 2, MidpointRounding.AwayFromZero);
    }

    public static void Main()
    {
        int feet = Convert.ToInt32(Console.ReadLine());
        double result = ConvertFeetToCentimeters(feet);
        Console.WriteLine(result);
    }
}

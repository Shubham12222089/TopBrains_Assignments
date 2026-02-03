
public class Program
{
    public static double CalculateCircleArea(double radius)
    {
        double area = Math.PI * radius * radius;
        return Math.Round(area, 2, MidpointRounding.AwayFromZero);
    }

    public static void Main()
    {
        double radius = Convert.ToDouble(Console.ReadLine());
        double result = CalculateCircleArea(radius);
        Console.WriteLine(result);
    }
}


public class Program
{
    public static double ComputeTotalArea(string[] shapes)
    {
        double totalArea = 0;

        foreach (string s in shapes)
        {
            if (string.IsNullOrWhiteSpace(s))
                continue;

            string[] parts = s.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Shape shape = null;

            if (parts[0] == "C")
            {
                double r = double.Parse(parts[1]);
                shape = new Circle(r);
            }
            else if (parts[0] == "R")
            {
                double w = double.Parse(parts[1]);
                double h = double.Parse(parts[2]);
                shape = new Rectangle(w, h);
            }
            else if (parts[0] == "T")
            {
                double b = double.Parse(parts[1]);
                double h = double.Parse(parts[2]);
                shape = new Triangle(b, h);
            }

            if (shape != null)
            {
                totalArea += shape.GetArea(); // polymorphic call
            }
        }

        return Math.Round(totalArea, 2, MidpointRounding.AwayFromZero);
    }

    public static void Main()
    {
        int n = Convert.ToInt32(Console.ReadLine());
        string[] shapes = new string[n];

        for (int i = 0; i < n; i++)
        {
            shapes[i] = Console.ReadLine();
        }

        double result = ComputeTotalArea(shapes);
        Console.WriteLine(result);
    }
}
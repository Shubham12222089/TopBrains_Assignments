using System;

namespace CalculatorApp
{
    class Program
    {
        static void Main()
        {
            Calculator calc = new Calculator();

            Console.WriteLine("==== Simple Calculator ====");
            Console.Write("Enter First Number: ");
            int a = int.Parse(Console.ReadLine());

            Console.Write("Enter Second Number: ");
            int b = int.Parse(Console.ReadLine());

            Console.Write("Choose Operation (+, -, *, /): ");
            string op = Console.ReadLine();

            try
            {
                int result = op switch
                {
                    "+" => calc.Add(a, b),
                    "-" => calc.Subtract(a, b),
                    "*" => calc.Multiply(a, b),
                    "/" => calc.Divide(a, b),
                    _ => throw new InvalidOperationException("Invalid Operator")
                };

                Console.WriteLine($"Result: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}

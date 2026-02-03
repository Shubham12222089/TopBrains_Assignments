
public class ExpressionEvaluator
{
    public static string Evaluate(string expression)
    {

        if (string.IsNullOrWhiteSpace(expression)) return "Error:InvalidExpression";

        string[] parts = expression.Split(' ');

        if (parts.Length != 3) return "Error:InvalidExpression";
   
        if (!int.TryParse(parts[0], out int a) || !int.TryParse(parts[2], out int b)) return "Error:InvalidNumber";

        string op = parts[1];

        switch (op)
        {
            case "+":
                return (a + b).ToString();

            case "-":
                return (a - b).ToString();

            case "*":
                return (a * b).ToString();

            case "/":
                if (b == 0)
                    return "Error:DivideByZero";
                return (a / b).ToString();

            default:
                return "Error:UnknownOperator";
        }
    }
}
public class Program
{
    static void Main()
    {
        string[] expressions =
        {
            "10 + 5",
            "10 / 0",
            "a + 2",
            "10 ^ 2",
            "10 +",
            "8 * 4"
        };

        foreach (string exp in expressions)
        {
            Console.WriteLine($"{exp} => {ExpressionEvaluator.Evaluate(exp)}");
        }
    }
}
namespace WebCalculator.Models
{
    public class Calculator
    {
        public int Add(int a, int b) => a + b;
        public int Sub(int a, int b) => a - b;
        public int Mul(int a, int b) => a * b;

        public double Div(int a, int b)
        {
            if (b == 0)
                throw new DivideByZeroException();
            return (double)a / b;
        }
    }
}

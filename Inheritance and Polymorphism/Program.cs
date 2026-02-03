public class Program
{
    public static decimal ComputeTotalPayroll(string[] employees)
    {
        decimal totalPay = 0;

        foreach (string emp in employees)
        {
            if (string.IsNullOrWhiteSpace(emp))
                continue;

            string[] parts = emp.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Employee employee = null;

            if (parts[0] == "H")
            {
                decimal rate = decimal.Parse(parts[1]);   //Parse converts string to decimal
                decimal hours = decimal.Parse(parts[2]);
                employee = new HourlyEmployee(rate, hours);
            }
            else if (parts[0] == "S")
            {
                decimal salary = decimal.Parse(parts[1]);
                employee = new SalariedEmployee(salary);
            }
            else if (parts[0] == "C")
            {
                decimal commission = decimal.Parse(parts[1]);
                decimal baseSalary = decimal.Parse(parts[2]);
                employee = new CommissionEmployee(commission, baseSalary);
            }

            if (employee != null)
            {
                totalPay += employee.GetPay(); // polymorphic call
            }
        }

        return Math.Round(totalPay, 2, MidpointRounding.AwayFromZero);
    }

    public static void Main()
    {
        int n = Convert.ToInt32(Console.ReadLine());
        string[] employees = new string[n];

        for (int i = 0; i < n; i++)
        {
            employees[i] = Console.ReadLine();
        }

        decimal result = ComputeTotalPayroll(employees);
        Console.WriteLine(result);
    }
}
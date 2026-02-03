

class Program
{
    static void Main()
    {
        int[] ids = { 1, 4, 5 };

        Dictionary<int, int> salaries = new Dictionary<int, int>
        {
            { 1, 20000 },
            { 4, 40000 },
            { 5, 15000 }
        };

        int totalSalary = 0;

        for (int i = 0; i < ids.Length; i++)
        {
            totalSalary += salaries[ids[i]];
        }

        Console.WriteLine(totalSalary);
    }
}
using Microsoft.EntityFrameworkCore.Diagnostics;
using DatabaseFirst.Models;
namespace DatabaseFirst
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Define Data Source
            AdventureWorks2025Context _context = new AdventureWorks2025Context();
            //Write the Query
            var query = _context.Employees.ToList();

            //Execute the Query

            foreach (var e in query)
            {
                Console.WriteLine($"Employee ID {e.BusinessEntityId} National ID Number {e.NationalIdnumber} Gender {e.Gender} Login ID {e.LoginId}");
            }
        }
    }
}
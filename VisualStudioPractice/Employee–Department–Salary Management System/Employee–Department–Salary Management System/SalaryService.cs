using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    internal class SalaryService
    {
        public static void AddSalary()
        {
            Console.Write("Employee Id: ");
            int eid = int.Parse(Console.ReadLine());

            if (DBManager.ds.Tables["Employees"].Rows.Find(eid) == null)
            {
                Console.WriteLine("Invalid Employee Id!");
                return;
            }

            DataRow row = DBManager.ds.Tables["Salaries"].NewRow();

            Console.Write("Salary Id: ");
            row["SalaryId"] = int.Parse(Console.ReadLine());
            row["EmployeeId"] = eid;
            Console.Write("Basic: ");
            row["Basic"] = decimal.Parse(Console.ReadLine());
            Console.Write("HRA: ");
            row["HRA"] = decimal.Parse(Console.ReadLine());
            Console.Write("Allowance: ");
            row["Allowance"] = decimal.Parse(Console.ReadLine());
            row["LastUpdated"] = DateTime.Now;

            DBManager.ds.Tables["Salaries"].Rows.Add(row);

            new SqlCommandBuilder(DBManager.daSal);
            DBManager.daSal.Update(DBManager.ds, "Salaries");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;



namespace ConsoleApp5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DBManager.LoadData();

            int ch;
            do
            {
                Console.WriteLine("\n===== HR MANAGEMENT SYSTEM =====");
                Console.WriteLine("1. Add Department");
                Console.WriteLine("2. Add Employee");
                Console.WriteLine("3. Add Salary");
                Console.WriteLine("4. View Employees");
                Console.WriteLine("5. Search Employee");
                Console.WriteLine("6. Delete Employee");
                Console.WriteLine("7. Delete Department");
                Console.WriteLine("0. Exit");

                ch = int.Parse(Console.ReadLine());

                switch (ch)
                {
                    case 1: DepartmentService.AddDepartment(); break;
                    case 2: EmployeeService.AddEmployee(); break;
                    case 3: SalaryService.AddSalary(); break;
                    case 4: EmployeeService.ViewAllEmployees(); break;
                    case 5: EmployeeService.SearchEmployee(); break;
                    case 6: EmployeeService.DeleteEmployee(); break;
                    case 7: DepartmentService.DeleteDepartment(); break;
                }

            } while (ch != 0);
        }
    }
}


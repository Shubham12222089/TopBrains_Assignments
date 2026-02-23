using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int choice;
            ConnectedEnv env = new ConnectedEnv();

            do
            {
                Console.WriteLine("\n====== COMPANY MANAGEMENT ======");
                Console.WriteLine("1. Add Department");
                Console.WriteLine("2. Add Employee");
                Console.WriteLine("3. View Departments");
                Console.WriteLine("4. View Employees");
                Console.WriteLine("5. View Employee with Department");
                Console.WriteLine("6. Update Employee Salary");
                Console.WriteLine("7. Delete Employee");
                Console.WriteLine("8. Delete Department");
                Console.WriteLine("0. Exit");
                Console.Write("Enter choice: ");

                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1: env.InsertDepartment(); break;
                    case 2: env.InsertEmployee(); break;
                    case 3: env.ShowAllDepartments(); break;
                    case 4: env.ShowAllEmployees(); break;
                    case 5: env.DisplayEmployeeWithDepartmentName(); break;
                    case 6: env.UpdateSalary(); break;
                    case 7: env.DeleteEmployee(); break;
                    case 8: env.DeleteDepartment(); break;
                }

            } while (choice != 0);
        }
    }
}

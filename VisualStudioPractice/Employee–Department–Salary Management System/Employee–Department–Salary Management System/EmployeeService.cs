using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    internal class EmployeeService
    {
        public static void AddEmployee()
        {
            Console.Write("Department Id: ");
            int did = int.Parse(Console.ReadLine());

            if (DBManager.ds.Tables["Departments"].Rows.Find(did) == null)
            {
                Console.WriteLine("Invalid Department Id!");
                return;
            }

            DataRow row = DBManager.ds.Tables["Employees"].NewRow();

            Console.Write("Employee Id: ");
            row["EmployeeId"] = int.Parse(Console.ReadLine());
            Console.Write("Name: ");
            row["EmployeeName"] = Console.ReadLine();
            Console.Write("Email: ");
            row["Email"] = Console.ReadLine();
            row["DepartmentId"] = did;
            Console.Write("Designation: ");
            row["Designation"] = Console.ReadLine();

            DBManager.ds.Tables["Employees"].Rows.Add(row);

            new SqlCommandBuilder(DBManager.daEmp);
            DBManager.daEmp.Update(DBManager.ds, "Employees");
        }

        public static void ViewAllEmployees()
        {
            foreach (DataRow emp in DBManager.ds.Tables["Employees"].Rows)
            {
                DataRow dept = emp.GetParentRow("Dept_Emp");
                Console.WriteLine($"{emp["EmployeeName"]} | {dept["DepartmentName"]}");
            }
        }

        public static void SearchEmployee()
        {
            Console.Write("Enter name: ");
            string name = Console.ReadLine();

            DataView dv = new DataView(DBManager.ds.Tables["Employees"]);
            dv.RowFilter = $"EmployeeName LIKE '%{name}%'";

            foreach (DataRowView row in dv)
                Console.WriteLine(row["EmployeeName"]);
        }

        public static void DeleteEmployee()
        {
            Console.Write("Employee Id: ");
            int id = int.Parse(Console.ReadLine());

            DataRow emp = DBManager.ds.Tables["Employees"].Rows.Find(id);

            foreach (DataRow sal in emp.GetChildRows("Emp_Salary"))
                sal.Delete();

            emp.Delete();

            new SqlCommandBuilder(DBManager.daEmp);
            new SqlCommandBuilder(DBManager.daSal);

            DBManager.daSal.Update(DBManager.ds, "Salaries");
            DBManager.daEmp.Update(DBManager.ds, "Employees");
        }
    }
}
 
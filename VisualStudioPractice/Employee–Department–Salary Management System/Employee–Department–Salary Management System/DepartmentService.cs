using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    internal class DepartmentService
    {
        public static void AddDepartment()
        {
            DataRow row = DBManager.ds.Tables["Departments"].NewRow();

            Console.Write("Department Id: ");
            row["DepartmentId"] = int.Parse(Console.ReadLine());
            Console.Write("Name: ");
            row["DepartmentName"] = Console.ReadLine();
            Console.Write("Location: ");
            row["Location"] = Console.ReadLine();

            DBManager.ds.Tables["Departments"].Rows.Add(row);

            new SqlCommandBuilder(DBManager.daDept);
            DBManager.daDept.Update(DBManager.ds, "Departments");
        }

        public static void DeleteDepartment()
        {
            Console.Write("Department Id: ");
            int id = int.Parse(Console.ReadLine());

            DataRow dept = DBManager.ds.Tables["Departments"].Rows.Find(id);

            if (dept.GetChildRows("Dept_Emp").Length > 0)
            {
                Console.WriteLine("Cannot delete department – employees assigned!");
                return;
            }

            dept.Delete();
            new SqlCommandBuilder(DBManager.daDept);
            DBManager.daDept.Update(DBManager.ds, "Departments");
        }
    }
}

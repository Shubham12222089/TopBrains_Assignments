using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    internal class DBManager
    {
        public static string conString = "Data Source=.\\SQLEXPRESS;Initial Catalog=HRDB;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        public static SqlConnection con = new SqlConnection(conString);
        public static DataSet ds = new DataSet();

        public static SqlDataAdapter daDept;
        public static SqlDataAdapter daEmp;
        public static SqlDataAdapter daSal;

        public static void LoadData()
        {
            daDept = new SqlDataAdapter("SELECT * FROM Departments", con);
            daEmp = new SqlDataAdapter("SELECT * FROM Employees", con);
            daSal = new SqlDataAdapter("SELECT * FROM Salaries", con);

            daDept.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            daEmp.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            daSal.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            daDept.Fill(ds, "Departments");
            daEmp.Fill(ds, "Employees");
            daSal.Fill(ds, "Salaries");

            CreateRelations();
        }

        private static void CreateRelations()
        {
            ds.Relations.Add("Dept_Emp",
                ds.Tables["Departments"].Columns["DepartmentId"],
                ds.Tables["Employees"].Columns["DepartmentId"]);

            ds.Relations.Add("Emp_Salary",
                ds.Tables["Employees"].Columns["EmployeeId"],
                ds.Tables["Salaries"].Columns["EmployeeId"]);
        }
    }
}

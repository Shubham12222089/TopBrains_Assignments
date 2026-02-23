using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace ConsoleApp4
{
    internal class ConnectedEnv
    {
        
        string conString = "Data Source=.\\SQLEXPRESS;Initial Catalog=CompanyDB;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        
        public void InsertDepartment()
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            Console.WriteLine("Connection Opened");
            Console.WriteLine("Enter DepartmentId: ");
            int depId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Department Name: ");
            string depName = Console.ReadLine();
            Console.WriteLine("Enter Department Location: ");
            string depLoc = Console.ReadLine();

            string sqlCmd = "INSERT INTO DEPARTMENTS VALUES(" + depId + ",'" + depName + "','" + depLoc + "')";
            SqlCommand cmd = new SqlCommand(sqlCmd, con);
            int rows = cmd.ExecuteNonQuery();
            Console.WriteLine($"{rows} rows inserted");
            con.Close();
            Console.WriteLine("Connected closed");
        }
        public void InsertEmployee()
        {
            SqlConnection con = new SqlConnection(conString);

            try
            {
                con.Open();
                Console.WriteLine("Connection Opened");
                Console.Write("Enter Employee Id: ");
                int empId = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter Employee Name: ");
                string empName = Console.ReadLine();
                Console.Write("Enter Employee Salary: ");
                double empSal = Convert.ToDouble(Console.ReadLine());
                Console.Write("Enter Department Id: ");
                int depId = Convert.ToInt32(Console.ReadLine());        
                string checkSql = "SELECT COUNT(*) FROM Departments WHERE DepartmentId=@did";
                
                SqlCommand checkCmd = new SqlCommand(checkSql, con);
                checkCmd.Parameters.AddWithValue("@did", depId);

                int deptCount = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (deptCount == 0)
                {
                    Console.WriteLine("Invalid Department Id!");
                    return;
                }

              
                string insertSql = "INSERT INTO Employees VALUES (@eid, @ename, @sal, @did)";

                SqlCommand insertCmd = new SqlCommand(insertSql, con);
                insertCmd.Parameters.AddWithValue("@eid", empId);
                insertCmd.Parameters.AddWithValue("@ename", empName);
                insertCmd.Parameters.AddWithValue("@sal", empSal);
                insertCmd.Parameters.AddWithValue("@did", depId);

                int rows = insertCmd.ExecuteNonQuery();
                Console.WriteLine($"{rows} row(s) inserted.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Database Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
                Console.WriteLine("Connection Closed");
            }
        }

        public void DeleteDepartment()
        {
            SqlConnection con = new SqlConnection(conString);
            
            try
            {
                con.Open();
                Console.WriteLine("Connection Opened");

                Console.Write("Enter Department Id: ");
                int depId = Convert.ToInt32(Console.ReadLine());

                string checkSql = "SELECT COUNT(*) FROM Employees WHERE DepartmentId=@id";
                SqlCommand checkCmd = new SqlCommand(checkSql, con);
                checkCmd.Parameters.AddWithValue("@id", depId);

                int empCount = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (empCount > 0)
                {
                    Console.WriteLine("Cannot delete department – employees assigned!");
                    return;
                }          
                string deleteSql = "DELETE FROM Departments WHERE DepartmentId=@id";
                SqlCommand deleteCmd = new SqlCommand(deleteSql, con);
                deleteCmd.Parameters.AddWithValue("@id", depId);

                int rows = deleteCmd.ExecuteNonQuery();
                Console.WriteLine($"{rows} row(s) deleted.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Database Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
                Console.WriteLine("Connection Closed");
            }
        }

        public void ShowAllDepartments()
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            Console.WriteLine("Connection Opened");

            string sqlCmd = "SELECT * FROM DEPARTMENTS";
            SqlCommand cmd = new SqlCommand(sqlCmd, con);
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Console.WriteLine($"DepartmentId: {rdr[0]}, DepartmentName: {rdr[1]}, Location: {rdr[2]}");
            }
            con.Close();
            Console.WriteLine("Connected closed");
        }
        public void ShowAllEmployees()
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            Console.WriteLine("Connection Opened");

            string sqlCmd = "SELECT * FROM EMPLOYEES";
            SqlCommand cmd = new SqlCommand(sqlCmd, con);
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Console.WriteLine($"EmployeeId: {rdr[0]}, EmployeetName: {rdr[1]}, Salary: {rdr[2]}, DepartmentId: {rdr[3]}");
            }
            con.Close();
            Console.WriteLine("Connected closed");
        }
        public void DisplayEmployeeWithDepartmentName()
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            Console.WriteLine("Connection Opened");

            string sqlCmd = "  SELECT e.EmployeeId," +
                            "  e.EmployeeName,e.Salary, d.DepartmentName " +
                            "  FROM Employees e " +
                            "  JOIN Departments d " +
                            "  ON e.DepartmentId = d.DepartmentId;";
            SqlCommand cmd = new SqlCommand(sqlCmd, con);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Console.WriteLine($"EmployeeId: {rdr[0]}, EmployeetName: {rdr[1]}, Salary: {rdr[2]}, DepartmentName: {rdr[3]}");

            }
            con.Close();
            Console.WriteLine("Connection closed");

        }
        public void UpdateSalary()
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            Console.WriteLine("Connection Opened");
            Console.WriteLine("Enter ID: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter salary: ");
            double sal = Convert.ToDouble(Console.ReadLine());
            string sqlCmd = $"  UPDATE EMPLOYEES" +
                            "  SET SALARY = {sal}" +                       
                            "  WHERE EMPLOYEEID = {id};";
            SqlCommand cmd = new SqlCommand(sqlCmd, con);
            int rows = cmd.ExecuteNonQuery();
            Console.WriteLine($"{rows} rows updated");
            con.Close();
            Console.WriteLine("Connected closed");
        }
        public void DeleteEmployee()
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            Console.WriteLine("Connection Opened");
            Console.WriteLine("Enter ID: ");
            int id = Convert.ToInt32(Console.ReadLine());

            string sqlCmd = $"DELETE FROM EMPLOYEES WHERE EMPLOYEEID={id}";
            SqlCommand cmd = new SqlCommand(sqlCmd, con);
            int rows = cmd.ExecuteNonQuery();
            Console.WriteLine($"{rows} rows deleted");
            con.Close();
            Console.WriteLine("Connected closed");
        }



    }
}

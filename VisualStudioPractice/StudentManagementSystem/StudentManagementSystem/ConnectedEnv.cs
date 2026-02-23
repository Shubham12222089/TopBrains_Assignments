using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace ConsoleApp3
{
    internal class ConnectedEnv
    {
        string ConString = "Data Source=.\\SQLEXPRESS;Initial Catalog=MSMS;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        public void GetAllStudents()
        {

            string sqlCommandText = "SELECT * FROM Students";
            SqlConnection sqlConnection = new SqlConnection(ConString);
            sqlConnection.Open();
            Console.WriteLine("Connection Opened\n");
            SqlCommand cmd = new SqlCommand(sqlCommandText, sqlConnection);
            SqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                int id = Convert.ToInt32(dataReader["Id"]);
                string name = dataReader["Name"].ToString();
                string email = dataReader["Email"].ToString();
                string course = dataReader["Course"].ToString();
                Console.WriteLine($"Id: {id}, Name: {name}, Email: {email}, Course: {course}");

            }
            sqlConnection.Close();
        }
        public void GetAllStudentsById()
        {
            Console.Write("Enter the Id of the student you want to search for: ");
            int id = Convert.ToInt32(Console.ReadLine());
            string sqlCmd = $"SELECT * FROM Students WHERE Id={id}";
            SqlConnection sqlConnection = new SqlConnection(ConString);
            sqlConnection.Open();
            Console.WriteLine("Connection Opened\n");
            SqlCommand cmd = new SqlCommand(sqlCmd, sqlConnection);
            SqlDataReader dataReader1 = cmd.ExecuteReader();
            while (dataReader1.Read())
            {
                string name = dataReader1["Name"].ToString();
                string email = dataReader1["Email"].ToString();
                string course = dataReader1["Course"].ToString();
                Console.WriteLine($"Id: {id}, Name: {name}, Email: {email}, Course: {course}");
            }
            sqlConnection.Close();
        }
        public void GetAllStudentsByCourse()
        {
            Console.Write("Enter the Course of the student you want to search for: ");
            string courseToSearch = Console.ReadLine();
            string sqlCmd = "SELECT * FROM Students WHERE Course=" + "'" + courseToSearch + "'";
            SqlConnection sqlConnection = new SqlConnection(ConString);
            sqlConnection.Open();
            Console.WriteLine("Connection Opened\n");
            SqlCommand cmd = new SqlCommand(sqlCmd, sqlConnection);
            SqlDataReader dataReader3 = cmd.ExecuteReader();
            while (dataReader3.Read())
            {
                int id = Convert.ToInt32(dataReader3["Id"]);
                string name = dataReader3["Name"].ToString();
                string email = dataReader3["Email"].ToString();
                string course = dataReader3["Course"].ToString();
                Console.WriteLine($"Id: {id}, Name: {name}, Email: {email}, Course: {course}");
            }
            sqlConnection.Close();
        }
        public void AddNewStudent()
        {
            Console.Write("Enter Id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Email: ");
            string email = Console.ReadLine();
            Console.Write("Enter Course: ");
            string course = Console.ReadLine();

            string sqlCmd = "INSERT INTO Students (Id, Name, Email, Course) VALUES (" + id + "," + "'" + name + "'" + "," + "'" + email + "'" + "," + "'" + course + "'" + ")";
            SqlConnection sqlConnection = new SqlConnection(ConString);
            sqlConnection.Open();
            Console.WriteLine("Connection Opened\n");
            SqlCommand cmd = new SqlCommand(sqlCmd, sqlConnection);
            int rowsAffected = cmd.ExecuteNonQuery();
            Console.WriteLine($"{rowsAffected} row(s) inserted.");
            sqlConnection.Close();
            Console.WriteLine("Connection Closed and Data Inserted");
        }
        public void DeleteStudentById()
        {
            Console.Write("Enter the Id of the student you want to delete: ");
            int id = Convert.ToInt32(Console.ReadLine());
            string sqlCmd = "DELETE FROM Students WHERE Id=" + id;
            SqlConnection sqlConnection = new SqlConnection(ConString);
            sqlConnection.Open();
            Console.WriteLine("Connection Opened\n");
            SqlCommand cmd = new SqlCommand(sqlCmd, sqlConnection);
            int rowsAffected = cmd.ExecuteNonQuery();
            Console.WriteLine($"{rowsAffected} row(s) deleted.");
            sqlConnection.Close();
            Console.WriteLine("Connection Closed and Data Deleted");
        }
        public void UpdateStudentCourse()
        {
            Console.Write("Enter the Id of the student whose course you want to update: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter the new Course: ");
            string newCourse = Console.ReadLine();
            string sqlCmd = "UPDATE Students SET Course=" + "'" + newCourse + "'" + " WHERE Id=" + id;

            SqlConnection sqlConnection = new SqlConnection(ConString);
            sqlConnection.Open();
            Console.WriteLine("Connection Opened\n");

            SqlCommand cmd = new SqlCommand(sqlCmd, sqlConnection);
            int rowsAffected = cmd.ExecuteNonQuery();

            Console.WriteLine($"{rowsAffected} row(s) updated.");
            sqlConnection.Close();
            Console.WriteLine("Connection Closed and Data Updated");
        }
        public void UpdateStudentEmail()
        {
            Console.Write("Enter the Id of the student whose email you want to update: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter the new Email: ");
            string newEmail = Console.ReadLine();

            string sqlCmd = "UPDATE Students SET Email=" + "'" + newEmail + "'" + " WHERE Id=" + id;

            SqlConnection sqlConnection = new SqlConnection(ConString);
            sqlConnection.Open();
            Console.WriteLine("Connection Opened\n");
            SqlCommand cmd = new SqlCommand(sqlCmd, sqlConnection);

            int rowsAffected = cmd.ExecuteNonQuery();
            Console.WriteLine($"{rowsAffected} row(s) updated.");

            sqlConnection.Close();
            Console.WriteLine("Connection Closed and Data Updated");


        }
    }
}

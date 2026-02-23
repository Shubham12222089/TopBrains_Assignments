using System.Data.SqlClient;
public class Class1
{
    SqlConnection connection;
    SqlCommand cmd;
    SqlDataReader dataReader;
    public Class1()
    {
    }
    public void GetAllStudent()
    {
        // connection = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=MSMS;Integrated Security=True;Trust Server Certificate=True");
         connection = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=MSMS;Integrated Security=True;");
        connection.Open();
        cmd = new SqlCommand("SELECT * FROM Students", connection);
        dataReader = cmd.ExecuteReader();
        while (dataReader.Read())
        {
            int id = Convert.ToInt32(dataReader["Id"]);
            string name = dataReader["Name"].ToString();
            string email = dataReader["Email"].ToString();
            Console.WriteLine("ID: " + id + ", Name: " + name + ", Email: " + email);
        }
        connection.Close();
    }
    public static void Main(string[] args)
    {
        Class1 obj = new Class1();
        obj.GetAllStudent();
        Console.ReadLine();
    }


}

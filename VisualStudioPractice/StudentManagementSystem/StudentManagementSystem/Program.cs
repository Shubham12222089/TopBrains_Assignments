using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConnectedEnv conEnv = new ConnectedEnv();

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("1. Get All Students\n" +
                                  "2. Get Student by Id\n" +
                                  "3. Get Students by Course\n" +
                                  "4. Add New Student\n" +
                                  "5. Update Student Course\n" +
                                  "6. Delete Student Record By Id\n" +
                                  "7. Update Student Email By Id\n");
                Console.Write("Choose an option: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        conEnv.GetAllStudents();
                        break;
                    case 2:
                        conEnv.GetAllStudentsById();
                        break;
                    case 3:
                        conEnv.GetAllStudentsByCourse();
                        break;
                    case 4:
                        conEnv.AddNewStudent();
                        break;
                    case 5:
                        conEnv.UpdateStudentCourse();
                        break;
                    case 6:
                        conEnv.DeleteStudentById();
                        break;
                    case 7:
                        conEnv.UpdateStudentEmail();
                        break;
                    default:
                        Console.WriteLine("Invalid Choice");
                        break;
                }
                Console.WriteLine();
            }
            

        }
    }
}

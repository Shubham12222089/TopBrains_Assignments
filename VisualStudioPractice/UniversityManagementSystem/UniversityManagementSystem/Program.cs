namespace UniversityManagementSystem
{
    using System;
    using System.Linq;
    using UniversityManagementSystem.Data;
    using UniversityManagementSystem.Entities;

    class Program
    {
        static void Main()
        {
            using var context = new UniversityDbContext();

            while (true)
            {
                Console.WriteLine("\n===== University Management System =====");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Update Student");
                Console.WriteLine("3. Delete Student");
                Console.WriteLine("4. View All Students");
                Console.WriteLine("5. Search Student by Branch");
                Console.WriteLine("6. Display Branch-wise Student Count");
                Console.WriteLine("7. Exit");
                Console.Write("Enter choice: ");

                int choice = int.Parse(Console.ReadLine());

                try
                {
                    switch (choice)
                    {
                        case 1:
                            AddStudent(context);
                            break;

                        case 2:
                            UpdateStudent(context);
                            break;

                        case 3:
                            DeleteStudent(context);
                            break;

                        case 4:
                            ViewAllStudents(context);
                            break;

                        case 5:
                            SearchStudentByBranch(context);
                            break;

                        case 6:
                            BranchWiseStudentCount(context);
                            break;

                        case 7:
                            Console.WriteLine("Exiting Application...");
                            return;

                        default:
                            Console.WriteLine("Invalid choice");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("❌ Error: " + ex.Message);
                }
            }
        }

        // ================= ADD STUDENT =================
        static void AddStudent(UniversityDbContext context)
        {
            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Mobile: ");
            string mobile = Console.ReadLine();

            Console.Write("Branch Id: ");
            int branchId = int.Parse(Console.ReadLine());

            Console.Write("Course Id: ");
            int courseId = int.Parse(Console.ReadLine());

            var student = new Student
            {
                Name = name,
                Email = email,
                Mobile = mobile,
                BranchId = branchId,
                CourseId = courseId
            };

            context.Students.Add(student);
            context.SaveChanges();

            Console.WriteLine("✅ Student added successfully");
        }

        // ================= UPDATE STUDENT =================
        static void UpdateStudent(UniversityDbContext context)
        {
            Console.Write("Enter Student Id: ");
            int id = int.Parse(Console.ReadLine());

            var student = context.Students.Find(id);
            if (student == null)
            {
                Console.WriteLine("Student not found");
                return;
            }

            Console.Write("New Email: ");
            student.Email = Console.ReadLine();

            Console.Write("New Mobile: ");
            student.Mobile = Console.ReadLine();

            context.SaveChanges();
            Console.WriteLine("✅ Student updated");
        }

        // ================= DELETE STUDENT =================
        static void DeleteStudent(UniversityDbContext context)
        {
            Console.Write("Enter Student Id: ");
            int id = int.Parse(Console.ReadLine());

            var student = context.Students.Find(id);
            if (student == null)
            {
                Console.WriteLine("Student not found");
                return;
            }

            var address = context.Addresses.FirstOrDefault(a => a.StudentId == id);
            if (address != null)
                context.Addresses.Remove(address);

            context.Students.Remove(student);
            context.SaveChanges();

            Console.WriteLine("✅ Student deleted");
        }

        // ================= VIEW ALL STUDENTS =================
        static void ViewAllStudents(UniversityDbContext context)
        {
            var students = context.Students
                .Select(s => new
                {
                    s.StudentId,
                    s.Name,
                    s.Email,
                    Branch = s.Branch.Name,
                    Course = s.Course.CourseName
                })
                .ToList();

            foreach (var s in students)
            {
                Console.WriteLine($"{s.StudentId} | {s.Name} | {s.Email} | {s.Branch} | {s.Course}");
            }
        }

        // ================= SEARCH BY BRANCH =================
        static void SearchStudentByBranch(UniversityDbContext context)
        {
            Console.Write("Enter Branch Name: ");
            string branchName = Console.ReadLine();

            var students = context.Students
                .Where(s => s.Branch.Name == branchName)
                .Select(s => new { s.Name, s.Email })
                .ToList();

            foreach (var s in students)
            {
                Console.WriteLine($"{s.Name} - {s.Email}");
            }
        }

        // ================= BRANCH-WISE COUNT =================
        static void BranchWiseStudentCount(UniversityDbContext context)
        {
            var data = context.Students
                .GroupBy(s => s.Branch.Name)
                .Select(g => new
                {
                    Branch = g.Key,
                    Count = g.Count()
                })
                .ToList();

            foreach (var item in data)
            {
                Console.WriteLine($"{item.Branch} : {item.Count}");
            }
        }
    }

}

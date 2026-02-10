using System;
using System.Collections.Generic;
using System.Linq;

namespace University_Course_Registration_System
{
     // =========================
    // Program (Menu-Driven)
    // =========================
    class Program
    {
        static void Main()
        {
            UniversitySystem system = new UniversitySystem();
            bool exit = false;

            Console.WriteLine("Welcome to University Course Registration System");

            while (!exit)
            {
                Console.WriteLine("\n1. Add Course");
                Console.WriteLine("2. Add Student");
                Console.WriteLine("3. Register Student for Course");
                Console.WriteLine("4. Drop Student from Course");
                Console.WriteLine("5. Display All Courses");
                Console.WriteLine("6. Display Student Schedule");
                Console.WriteLine("7. Display System Summary");
                Console.WriteLine("8. Exit");

                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();

                try
                {
                    // TODO:
                    // Implement menu handling logic using switch-case
                    // Prompt user inputs
                    // Call appropriate UniversitySystem methods
                    switch (choice)
                    {
                        case "1": // Add Course
                            Console.Write("Enter Course Code: ");
                            string code = Console.ReadLine();

                            Console.Write("Enter Course Name: ");
                            string name = Console.ReadLine();

                            Console.Write("Enter Credits (1-4): ");
                            int credits;
                            if (!int.TryParse(Console.ReadLine(), out credits))
                            {
                                Console.WriteLine("Invalid credits.");
                                break;
                            }

                            Console.Write("Enter Max Capacity (10-100, default 50): ");
                            string capInput = Console.ReadLine();
                            int capacity = 50;
                            if (capInput != "")
                            {
                                if (!int.TryParse(capInput, out capacity))
                                {
                                    Console.WriteLine("Invalid capacity.");
                                    break;
                                }
                            }

                            Console.Write("Enter Prerequisites (comma-separated or Enter): ");
                            string prereqInput = Console.ReadLine();
                            List<string> prerequisites = new List<string>();

                            if (prereqInput != "")
                            {
                                string[] parts = prereqInput.Split(',');
                                foreach (string p in parts)
                                {
                                    prerequisites.Add(p.Trim());
                                }
                            }

                            system.AddCourse(code, name, credits, capacity, prerequisites);
                            Console.WriteLine("Course added successfully.");
                            break;

                        case "2": // Add Student
                            Console.Write("Enter Student ID: ");
                            string id = Console.ReadLine();

                            Console.Write("Enter Name: ");
                            string studentName = Console.ReadLine();

                            Console.Write("Enter Major: ");
                            string major = Console.ReadLine();

                            Console.Write("Enter Max Credits (default 18): ");
                            string maxCredInput = Console.ReadLine();
                            int maxCredits = 18;
                            if (maxCredInput != "")
                            {
                                if (!int.TryParse(maxCredInput, out maxCredits))
                                {
                                    Console.WriteLine("Invalid max credits.");
                                    break;
                                }
                            }

                            Console.Write("Enter Completed Courses (comma-separated or Enter): ");
                            string completedInput = Console.ReadLine();
                            List<string> completedCourses = new List<string>();

                            if (completedInput != "")
                            {
                                string[] parts = completedInput.Split(',');
                                foreach (string c in parts)
                                {
                                    completedCourses.Add(c.Trim());
                                }
                            }

                            system.AddStudent(id, studentName, major, maxCredits, completedCourses);
                            Console.WriteLine("Student added successfully.");
                            break;

                        case "3": 
                            Console.Write("Enter Student ID: ");
                            string regStudentId = Console.ReadLine();

                            Console.Write("Enter Course Code: ");
                            string regCourseCode = Console.ReadLine();

                            system.RegisterStudentForCourse(regStudentId, regCourseCode);
                            break;

                        case "4": 
                            Console.Write("Enter Student ID: ");
                            string dropStudentId = Console.ReadLine();

                            Console.Write("Enter Course Code: ");
                            string dropCourseCode = Console.ReadLine();

                            system.DropStudentFromCourse(dropStudentId, dropCourseCode);
                            break;

                        case "5": 
                            system.DisplayAllCourses();
                            break;

                        case "6": 
                            Console.Write("Enter Student ID: ");
                            string schedStudentId = Console.ReadLine();

                            system.DisplayStudentSchedule(schedStudentId);
                            break;

                        case "7": 
                            system.DisplaySystemSummary();
                            break;

                        case "8": 
                            exit = true;
                            Console.WriteLine("Exiting system. Goodbye!");
                            break;

                        default:
                            Console.WriteLine("Invalid choice. Try again.");
                            break;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}

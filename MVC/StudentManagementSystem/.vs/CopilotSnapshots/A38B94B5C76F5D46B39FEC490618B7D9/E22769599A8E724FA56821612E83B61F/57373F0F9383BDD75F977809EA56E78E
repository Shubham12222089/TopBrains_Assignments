using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Data
{
    public class StudentDbContext: DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options)
        : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }

        // Method to seed data programmatically (called from Program.cs)
        public void SeedData()
        {
            try
            {
                // Only seed if no data exists
                if (!Departments.Any())
                {
                    var dept1 = new Department { DepartmentName = "Computer Science", Location = "Building A" };
                    var dept2 = new Department { DepartmentName = "Information Technology", Location = "Building B" };
                    var dept3 = new Department { DepartmentName = "Business Administration", Location = "Building C" };

                    Departments.AddRange(dept1, dept2, dept3);
                    SaveChanges();

                    // Now seed courses using actual department IDs
                    if (!Courses.Any())
                    {
                        var course1 = new Course { CourseName = ".NET Full Stack Development", Duration = "6 Months", DepartmentId = dept1.DepartmentId };
                        var course2 = new Course { CourseName = "Angular Development", Duration = "4 Months", DepartmentId = dept1.DepartmentId };
                        var course3 = new Course { CourseName = "Cloud Computing", Duration = "6 Months", DepartmentId = dept2.DepartmentId };
                        var course4 = new Course { CourseName = "Cyber Security", Duration = "5 Months", DepartmentId = dept2.DepartmentId };
                        var course5 = new Course { CourseName = "Financial Management", Duration = "3 Months", DepartmentId = dept3.DepartmentId };

                        Courses.AddRange(course1, course2, course3, course4, course5);
                        SaveChanges();

                        // Now seed students using actual department and course IDs
                        if (!Students.Any())
                        {
                            Students.AddRange(
                                new Student
                                {
                                    Name = "Ravi Kumar",
                                    Email = "ravi@gmail.com",
                                    Age = 22,
                                    Gender = "Male",
                                    AdmissionDate = new DateTime(2024, 1, 15),
                                    DepartmentId = dept1.DepartmentId,
                                    CourseId = course1.CourseId
                                },
                                new Student
                                {
                                    Name = "Anjali Sharma",
                                    Email = "anjali@gmail.com",
                                    Age = 23,
                                    Gender = "Female",
                                    AdmissionDate = new DateTime(2024, 2, 10),
                                    DepartmentId = dept2.DepartmentId,
                                    CourseId = course3.CourseId
                                },
                                new Student
                                {
                                    Name = "Suresh Reddy",
                                    Email = "suresh@gmail.com",
                                    Age = 24,
                                    Gender = "Male",
                                    AdmissionDate = new DateTime(2024, 3, 5),
                                    DepartmentId = dept1.DepartmentId,
                                    CourseId = course2.CourseId
                                },
                                new Student
                                {
                                    Name = "Priya Nair",
                                    Email = "priya@gmail.com",
                                    Age = 21,
                                    Gender = "Female",
                                    AdmissionDate = new DateTime(2024, 4, 20),
                                    DepartmentId = dept3.DepartmentId,
                                    CourseId = course5.CourseId
                                }
                            );
                            SaveChanges();
                        }
                    }
                }
                else if (!Courses.Any())
                {
                    // Departments exist, but no courses
                    var depts = Departments.OrderBy(d => d.DepartmentId).ToList();
                    if (depts.Count >= 3)
                    {
                        var course1 = new Course { CourseName = ".NET Full Stack Development", Duration = "6 Months", DepartmentId = depts[0].DepartmentId };
                        var course2 = new Course { CourseName = "Angular Development", Duration = "4 Months", DepartmentId = depts[0].DepartmentId };
                        var course3 = new Course { CourseName = "Cloud Computing", Duration = "6 Months", DepartmentId = depts[1].DepartmentId };
                        var course4 = new Course { CourseName = "Cyber Security", Duration = "5 Months", DepartmentId = depts[1].DepartmentId };
                        var course5 = new Course { CourseName = "Financial Management", Duration = "3 Months", DepartmentId = depts[2].DepartmentId };

                        Courses.AddRange(course1, course2, course3, course4, course5);
                        SaveChanges();
                    }
                }
                else if (!Students.Any())
                {
                    // Departments and Courses exist, but no students
                    var depts = Departments.OrderBy(d => d.DepartmentId).ToList();
                    var courses = Courses.OrderBy(c => c.CourseId).ToList();

                    if (depts.Count >= 3 && courses.Count >= 5)
                    {
                        Students.AddRange(
                            new Student
                            {
                                Name = "Ravi Kumar",
                                Email = "ravi@gmail.com",
                                Age = 22,
                                Gender = "Male",
                                AdmissionDate = new DateTime(2024, 1, 15),
                                DepartmentId = depts[0].DepartmentId,
                                CourseId = courses[0].CourseId
                            },
                            new Student
                            {
                                Name = "Anjali Sharma",
                                Email = "anjali@gmail.com",
                                Age = 23,
                                Gender = "Female",
                                AdmissionDate = new DateTime(2024, 2, 10),
                                DepartmentId = depts[1].DepartmentId,
                                CourseId = courses[2].CourseId
                            },
                            new Student
                            {
                                Name = "Suresh Reddy",
                                Email = "suresh@gmail.com",
                                Age = 24,
                                Gender = "Male",
                                AdmissionDate = new DateTime(2024, 3, 5),
                                DepartmentId = depts[0].DepartmentId,
                                CourseId = courses[1].CourseId
                            },
                            new Student
                            {
                                Name = "Priya Nair",
                                Email = "priya@gmail.com",
                                Age = 21,
                                Gender = "Female",
                                AdmissionDate = new DateTime(2024, 4, 20),
                                DepartmentId = depts[2].DepartmentId,
                                CourseId = courses[4].CourseId
                            }
                        );
                        SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error for debugging
                System.Diagnostics.Debug.WriteLine($"Error seeding data: {ex.Message}");
                if (ex.InnerException != null)
                {
                    System.Diagnostics.Debug.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                throw;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships using Fluent API

            // Student -> Department (Many to One)
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Department)
                .WithMany(d => d.Students)
                .HasForeignKey(s => s.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Student -> Course (Many to One)
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Course)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Department -> Course (One to Many)
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Department)
                .WithMany(d => d.Courses)
                .HasForeignKey(c => c.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Note: Seed data has been moved to SeedData() method
            // This prevents conflicts with existing data during migrations
        }
    }
}

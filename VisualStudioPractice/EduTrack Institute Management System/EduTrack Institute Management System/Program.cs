using EduTrack_Institute_Management_System;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;

class Program
{
    static void Main()
    {
        try
        {
            using var context = new EduTrackDbContext();

            Console.WriteLine("===== EduTrack Institute Management System =====");

            // 1️⃣ Add Department
            var dept = new Department
            {
                DepartmentName = "Computer Science",
                Location = "Hyderabad",
                EstablishedYear = 2016
            };
            context.Departments.Add(dept);
            context.SaveChanges();

            // 2️⃣ Add Course
            var course = new Course
            {
                CourseTitle = "Full Stack .NET",
                CourseDuration = 6,
                Fees = 55000,
                DepartmentId = dept.DepartmentId
            };
            context.Courses.Add(course);
            context.SaveChanges();

            // 3️⃣ Add Instructor
            var instructor = new Instructor
            {
                InstructorName = "Ravi Kumar",
                Email = "ravi@edutrack.com",
                PhoneNumber = "9876543210",
                DepartmentId = dept.DepartmentId
            };
            context.Instructors.Add(instructor);
            context.SaveChanges();

            // 4️⃣ Add Student
            var student = new Student
            {
                FirstName = "Ananya",
                LastName = "Sharma",
                Email = "ananya@gmail.com",
                PhoneNumber = "9998887776",
                DateOfBirth = new DateTime(2001, 5, 10),
                Gender = "Female"
            };
            context.Students.Add(student);
            context.SaveChanges();

            // 5️⃣ Add Address (One-to-One)
            var address = new Address
            {
                StudentId = student.StudentId,
                Street = "Madhapur",
                City = "Hyderabad",
                State = "Telangana",
                Country = "India",
                PostalCode = "500081"
            };
            context.Addresses.Add(address);
            context.SaveChanges();

            // 6️⃣ Enrollment (Many-to-Many)
            var enrollment = new Enrollment
            {
                StudentId = student.StudentId,
                CourseId = course.CourseId,
                EnrollmentDate = DateTime.Now,
                Status = "Active"
            };
            context.Enrollments.Add(enrollment);
            context.SaveChanges();

            // 7️⃣ Add Payment
            var payment = new Payment
            {
                StudentId = student.StudentId,
                Amount = 20000,
                PaymentDate = DateTime.Now,
                PaymentMode = "UPI"
            };
            context.Payments.Add(payment);
            context.SaveChanges();

            Console.WriteLine("✔ Data Inserted Successfully\n");

            // ================= READ OPERATIONS =================

            Console.WriteLine("📌 Students with Courses:");
            var studentsWithCourses = context.Students
                .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Course)
                .AsNoTracking()
                .ToList();

            foreach (var s in studentsWithCourses)
            {
                Console.WriteLine($"{s.FirstName} {s.LastName}");
                foreach (var e in s.Enrollments)
                    Console.WriteLine($"  → {e.Course.CourseTitle}");
            }

            Console.WriteLine("\n📌 Students with Payments:");
            var studentsWithPayments = context.Students
                .Include(s => s.Payments)
                .ToList();

            foreach (var s in studentsWithPayments)
            {
                Console.WriteLine($"{s.FirstName} | Payments: {s.Payments.Count}");
            }

            // ================= UPDATE OPERATIONS =================

            student.Email = "ananya.updated@gmail.com";
            course.Fees = 60000;
            payment.Amount = 25000;

            context.SaveChanges();
            Console.WriteLine("\n✔ Update Operations Completed");

            // ================= TRANSACTION EXAMPLE =================

            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.Payments.Add(new Payment
                {
                    StudentId = student.StudentId,
                    Amount = 15000,
                    PaymentDate = DateTime.Now,
                    PaymentMode = "Card"
                });

                context.SaveChanges();
                transaction.Commit();
                Console.WriteLine("✔ Transaction Committed");
            }
            catch
            {
                transaction.Rollback();
                Console.WriteLine("❌ Transaction Rolled Back");
            }

            // ================= DELETE OPERATIONS =================

            context.Enrollments.Remove(enrollment);
            context.Courses.Remove(course);
            context.Students.Remove(student);
            context.SaveChanges();

            Console.WriteLine("\n✔ Delete Operations Completed");
            Console.WriteLine("\n===== Program Completed Successfully =====");
        }
        catch (Exception ex)
        {
            Console.WriteLine("❌ Error Occurred:");
            Console.WriteLine(ex.Message);
        }
    }
}

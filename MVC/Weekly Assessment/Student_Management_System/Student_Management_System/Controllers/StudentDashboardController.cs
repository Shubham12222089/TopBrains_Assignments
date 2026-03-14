using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Management_System.Data;
using Student_Management_System.Models;

namespace Student_Management_System.Controllers
{
    public class StudentDashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentDashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.TotalStudents = _context.Students.Count();
            ViewBag.ActiveCourses = _context.Courses.Count();
            ViewBag.TotalDepartments = _context.Departments.Count();
            return View();
        }

        public IActionResult Profile()
        {
            // For demo purposes, get the first student
            // In a real application, you would use authentication to get the logged-in user
            var student = _context.Students
                .Include(s => s.Department)
                .Include(s => s.Course)
                .FirstOrDefault();

            if (student == null)
                return RedirectToAction("Index");

            return View(student);
        }

        [HttpPost]
        public IActionResult UpdateProfile(int StudentId, string PhoneNumber, string Address)
        {
            var student = _context.Students.Find(StudentId);
            if (student != null)
            {
                student.PhoneNumber = PhoneNumber;
                student.Address = Address;
                _context.Students.Update(student);
                _context.SaveChanges();

                ViewBag.SuccessMessage = "Profile updated successfully!";
            }

            return RedirectToAction("Profile");
        }

        public IActionResult CourseDetails()
        {
            // Get the first student's course details
            var student = _context.Students
                .Include(s => s.Course)
                .ThenInclude(c => c.Department)
                .FirstOrDefault();

            if (student?.Course == null)
                return Content("No course assigned");

            return View(student.Course);
        }
    }
}

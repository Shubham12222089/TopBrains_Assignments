using Microsoft.AspNetCore.Mvc;
using Student_Management_System.Data;

namespace Student_Management_System.Controllers
{
    public class TeacherDashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeacherDashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.TotalStudents = _context.Students.Count();
            ViewBag.TotalCourses = _context.Courses.Count();
            ViewBag.TotalDepartments = _context.Departments.Count();
            return View();
        }
    }
}

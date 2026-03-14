using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Models;
using StudentManagementSystem.UnitOfWork;
using System.Diagnostics;

namespace StudentManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            ViewBag.TotalStudents = _unitOfWork.Students.GetAll().Count();
            ViewBag.TotalDepartments = _unitOfWork.Departments.GetAll().Count();
            ViewBag.TotalCourses = _unitOfWork.Courses.GetAll().Count();
            ViewBag.RecentStudents = _unitOfWork.Students.GetTop5RecentAdmissions();
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

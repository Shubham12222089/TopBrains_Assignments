using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Models;
using StudentManagementSystem.UnitOfWork;

namespace StudentManagementSystem.Controllers
{
    public class CourseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var courses = _unitOfWork.Courses.GetCoursesWithDepartment();
            return View(courses);
        }

        public IActionResult Create()
        {
            ViewBag.Departments = _unitOfWork.Departments.GetAll();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Course course)
        {
            ModelState.Remove("Department");
            
            if (ModelState.IsValid)
            {
                _unitOfWork.Courses.Insert(course);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }

            ViewBag.Departments = _unitOfWork.Departments.GetAll();
            return View(course);
        }

        public IActionResult Details(int id)
        {
            var course = _unitOfWork.Courses.GetCourseWithDetails(id);
            if (course == null)
                return NotFound();

            return View(course);
        }

        public IActionResult Edit(int id)
        {
            var course = _unitOfWork.Courses.GetById(id);
            if (course == null)
                return NotFound();

            ViewBag.Departments = _unitOfWork.Departments.GetAll();
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Course course)
        {
            ModelState.Remove("Department");
            
            if (ModelState.IsValid)
            {
                _unitOfWork.Courses.Update(course);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }

            ViewBag.Departments = _unitOfWork.Departments.GetAll();
            return View(course);
        }

        public IActionResult Delete(int id)
        {
            var course = _unitOfWork.Courses.GetCourseWithDetails(id);
            if (course == null)
                return NotFound();

            return View(course);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var course = _unitOfWork.Courses.GetCourseWithDetails(id);

                if (course == null)
                {
                    TempData["ErrorMessage"] = "Course not found!";
                    return RedirectToAction("Index");
                }

                // Check if course has enrolled students
                if (course.Students?.Any() == true)
                {
                    TempData["ErrorMessage"] = $"Cannot delete course '{course.CourseName}'! It has {course.Students.Count} enrolled student(s). Please unenroll them first.";
                    return RedirectToAction("Index");
                }

                _unitOfWork.Courses.Delete(id);
                _unitOfWork.Save();
                TempData["SuccessMessage"] = $"Course '{course.CourseName}' deleted successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error deleting course: {ex.InnerException?.Message ?? ex.Message}";
            }

            return RedirectToAction("Index");
        }

        public IActionResult ByDepartment(int id)
        {
            var courses = _unitOfWork.Courses.GetCoursesByDepartment(id);
            var department = _unitOfWork.Departments.GetById(id);
            ViewBag.DepartmentName = department?.DepartmentName;
            return View("Index", courses);
        }
    }
}

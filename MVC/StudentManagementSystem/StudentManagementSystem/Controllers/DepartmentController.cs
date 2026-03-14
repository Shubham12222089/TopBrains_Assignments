using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Models;
using StudentManagementSystem.UnitOfWork;

namespace StudentManagementSystem.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var departments = _unitOfWork.Departments.GetDepartmentsWithStudents();
            return View(departments);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Departments.Insert(department);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(department);
        }

        public IActionResult Details(int id)
        {
            var department = _unitOfWork.Departments.GetDepartmentWithDetails(id);
            if (department == null)
                return NotFound();

            return View(department);
        }

        public IActionResult Edit(int id)
        {
            var department = _unitOfWork.Departments.GetById(id);
            if (department == null)
                return NotFound();

            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Departments.Update(department);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(department);
        }

        public IActionResult Delete(int id)
        {
            var department = _unitOfWork.Departments.GetById(id);
            if (department == null)
                return NotFound();

            return View(department);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var department = _unitOfWork.Departments.GetDepartmentWithDetails(id);

                if (department == null)
                {
                    TempData["ErrorMessage"] = "Department not found!";
                    return RedirectToAction("Index");
                }

                // Check if department has students or courses
                if (department.Students?.Any() == true || department.Courses?.Any() == true)
                {
                    var studentCount = department.Students?.Count ?? 0;
                    var courseCount = department.Courses?.Count ?? 0;
                    TempData["ErrorMessage"] = $"Cannot delete department '{department.DepartmentName}'! It has {studentCount} student(s) and {courseCount} course(s). Please reassign or delete them first.";
                    return RedirectToAction("Index");
                }

                _unitOfWork.Departments.Delete(id);
                _unitOfWork.Save();
                TempData["SuccessMessage"] = $"Department '{department.DepartmentName}' deleted successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error deleting department: {ex.InnerException?.Message ?? ex.Message}";
            }

            return RedirectToAction("Index");
        }
    }
}

using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Repositories
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        IEnumerable<Course> GetCoursesWithDepartment();
        Course? GetCourseWithDetails(int id);
        IEnumerable<Course> GetCoursesByDepartment(int departmentId);
    }

    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        public CourseRepository(StudentDbContext context) : base(context)
        {
        }

        public IEnumerable<Course> GetCoursesWithDepartment()
        {
            return _context.Courses
                .Include(c => c.Department)
                .Include(c => c.Students)
                .ToList();
        }

        public Course? GetCourseWithDetails(int id)
        {
            return _context.Courses
                .Include(c => c.Department)
                .Include(c => c.Students)
                .FirstOrDefault(c => c.CourseId == id);
        }

        public IEnumerable<Course> GetCoursesByDepartment(int departmentId)
        {
            return _context.Courses
                .Include(c => c.Department)
                .Where(c => c.DepartmentId == departmentId)
                .ToList();
        }
    }
}

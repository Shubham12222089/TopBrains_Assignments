using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Repositories
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        IEnumerable<Student> GetStudentsWithDepartment();
        Student? GetStudentWithDetails(int id);
        IEnumerable<Student> SearchByName(string name);
        IEnumerable<Student> GetByDepartment(int departmentId);
        IEnumerable<Student> GetByCourse(int courseId);
        IEnumerable<Student> GetStudentsOlderThan(int age);
        IEnumerable<Student> GetStudentsAdmittedAfter(DateTime date);
        IEnumerable<Student> GetTop5RecentAdmissions();
    }

    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(StudentDbContext context) : base(context)
        {
        }

        public IEnumerable<Student> GetStudentsWithDepartment()
        {
            return _context.Students
                .Include(s => s.Department)
                .Include(s => s.Course)
                .OrderBy(s => s.Name)
                .ToList();
        }

        public Student? GetStudentWithDetails(int id)
        {
            return _context.Students
                .Include(s => s.Department)
                .Include(s => s.Course)
                .FirstOrDefault(s => s.StudentId == id);
        }

        public IEnumerable<Student> SearchByName(string name)
        {
            return _context.Students
                .Include(s => s.Department)
                .Include(s => s.Course)
                .Where(s => s.Name.Contains(name))
                .ToList();
        }

        public IEnumerable<Student> GetByDepartment(int departmentId)
        {
            return _context.Students
                .Include(s => s.Department)
                .Include(s => s.Course)
                .Where(s => s.DepartmentId == departmentId)
                .ToList();
        }

        public IEnumerable<Student> GetByCourse(int courseId)
        {
            return _context.Students
                .Include(s => s.Department)
                .Include(s => s.Course)
                .Where(s => s.CourseId == courseId)
                .ToList();
        }

        public IEnumerable<Student> GetStudentsOlderThan(int age)
        {
            return _context.Students
                .Include(s => s.Department)
                .Where(s => s.Age > age)
                .ToList();
        }

        public IEnumerable<Student> GetStudentsAdmittedAfter(DateTime date)
        {
            return _context.Students
                .Include(s => s.Department)
                .Where(s => s.AdmissionDate > date)
                .ToList();
        }

        public IEnumerable<Student> GetTop5RecentAdmissions()
        {
            return _context.Students
                .Include(s => s.Department)
                .Include(s => s.Course)
                .OrderByDescending(s => s.AdmissionDate)
                .Take(5)
                .ToList();
        }
    }
}

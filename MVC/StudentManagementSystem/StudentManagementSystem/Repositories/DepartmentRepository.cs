using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Repositories
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        IEnumerable<Department> GetDepartmentsWithStudents();
        Department? GetDepartmentWithDetails(int id);
        IEnumerable<object> GetStudentCountPerDepartment();
    }

    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(StudentDbContext context) : base(context)
        {
        }

        public IEnumerable<Department> GetDepartmentsWithStudents()
        {
            return _context.Departments
                .Include(d => d.Students)
                .Include(d => d.Courses)
                .ToList();
        }

        public Department? GetDepartmentWithDetails(int id)
        {
            return _context.Departments
                .Include(d => d.Students)
                .Include(d => d.Courses)
                .FirstOrDefault(d => d.DepartmentId == id);
        }

        public IEnumerable<object> GetStudentCountPerDepartment()
        {
            return _context.Students
                .GroupBy(s => s.DepartmentId)
                .Select(g => new
                {
                    DepartmentId = g.Key,
                    DepartmentName = g.First().Department!.DepartmentName,
                    TotalStudents = g.Count()
                })
                .ToList();
        }
    }
}

using StudentManagementSystem.Data;
using StudentManagementSystem.Repositories;

namespace StudentManagementSystem.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StudentDbContext _context;
        private IStudentRepository? _studentRepository;
        private IDepartmentRepository? _departmentRepository;
        private ICourseRepository? _courseRepository;

        public UnitOfWork(StudentDbContext context)
        {
            _context = context;
        }

        public IStudentRepository Students
        {
            get
            {
                _studentRepository ??= new StudentRepository(_context);
                return _studentRepository;
            }
        }

        public IDepartmentRepository Departments
        {
            get
            {
                _departmentRepository ??= new DepartmentRepository(_context);
                return _departmentRepository;
            }
        }

        public ICourseRepository Courses
        {
            get
            {
                _courseRepository ??= new CourseRepository(_context);
                return _courseRepository;
            }
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

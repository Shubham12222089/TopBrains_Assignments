using StudentManagementSystem.Repositories;

namespace StudentManagementSystem.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IStudentRepository Students { get; }
        IDepartmentRepository Departments { get; }
        ICourseRepository Courses { get; }

        int Save();
    }
}

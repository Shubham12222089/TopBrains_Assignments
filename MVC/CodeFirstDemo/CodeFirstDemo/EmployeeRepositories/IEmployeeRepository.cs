using CodeFirstDemo.Models;

namespace CodeFirstDemo.EmployeeRepositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAll();
        Task<Employee> GetEmployeeById(int? id);

        Task Add(Employee employee);
        Task Update(int id,Employee employee);
        Task Delete(Employee employee);

    }
}

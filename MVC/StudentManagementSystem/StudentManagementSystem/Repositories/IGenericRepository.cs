using System.Linq.Expressions;

namespace StudentManagementSystem.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T? GetById(int id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(int id);
        void Delete(T entity);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
    }
}

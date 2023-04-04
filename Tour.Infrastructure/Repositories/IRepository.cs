using System.Linq.Expressions;

namespace Tour.Infrastructure.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        IQueryable<T> GetAll(Expression<Func<T, bool>> expression = null);
        Task<T> GetByIdAsync(string id);
        Task<T> GetFirstAsync(Expression<Func<T, bool>> expression);
        int Count(Expression<Func<T, bool>> where);

        bool CheckContains(Expression<Func<T, bool>> predicate);

        T GetFirstAsyncCondition(Expression<Func<T, bool>> expression, string[] includes = null);

        IEnumerable<T> GetAllJoin(string[] includes = null);

        IEnumerable<T> GetMultiJoin(Expression<Func<T, bool>> predicate, string[] includes = null);

        IEnumerable<T> GetMultiPagingJoin(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 50, string[] includes = null);
    }
}

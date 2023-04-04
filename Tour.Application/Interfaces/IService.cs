using System.Linq.Expressions;

namespace Tour.Application.Interfaces
{
    public interface IService<TEntity, TDto>
    {
        Task AddAsync(TDto tDto);

        Task DeleteAsync(string id);

        Task<IEnumerable<TDto>> GetAll(Expression<Func<TDto, bool>> expression = null);

        Task<TDto> GetByIdAsync(string id);
        Task UpdateAsync(string id, TDto entityTDto);
        Task<TDto> GetFirstAsync(Expression<Func<TDto, bool>> expression);

        IEnumerable<TEntity> GetAllJoin(string[] includes = null);

        IEnumerable<TEntity> GetMultiJoin(Expression<Func<TEntity, bool>> predicate, string[] includes = null);

        IEnumerable<TEntity> GetMultiPagingJoin(Expression<Func<TEntity, bool>> filter, out int total, int index = 0, int size = 50, string[] includes = null);
    }
}

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
        IEnumerable<TEntity> GetAllJoin(string[] includes = null);

        IEnumerable<TEntity> GetMultiJoin(Expression<Func<TEntity, bool>> predicate, string[] includes = null);

       
    }
}

using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Tour.Infrastructure.Data;

namespace Tour.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly TourDatabaseContext _context;
        private DbSet<T> _entity;

        public Repository(TourDatabaseContext context)
        {
            _context = context;
            _entity = _context.Set<T>();
        }


        public async Task AddAsync(T entity)
        {
            await _entity.AddAsync(entity);
        }

        public bool CheckContains(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public int Count(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(T entity)
        {
            _entity.Remove(entity);
            return Task.CompletedTask;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> expression)
        {
            var result = _entity.AsNoTracking();
            if (expression != null)
            {
                result = result.Where(expression);
            }
            return result;
        }

        public IEnumerable<T> GetAllJoin(string[] includes = null)
        {

            if (includes != null && includes.Count() > 0)
            {
                var query = _entity.Include(includes.First());
                foreach (var include in includes.Skip(1))
                {
                    query = query.Include(include);
                }

                return query.AsQueryable();
            }

            return _entity.AsQueryable();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _entity.FindAsync(id);
        }

        public async Task<T> GetFirstAsync(Expression<Func<T, bool>> expression)
        {
            return await _entity.FirstOrDefaultAsync(expression);
        }

        public T GetFirstAsyncCondition(Expression<Func<T, bool>> expression, string[] includes = null)
        {
            if (includes != null && includes.Count() > 0)
            {
                var query = _entity.Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return query.FirstOrDefault(expression);
            }
            return _entity.FirstOrDefault(expression);
        }

        public IEnumerable<T> GetMultiJoin(Expression<Func<T, bool>> predicate, string[] includes = null)
        {
            if (includes != null && includes.Count() > 0)
            {
                var query = _entity.Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return query.Where(predicate).AsQueryable();
            }

            return _entity.Where(predicate).AsQueryable();
        }

        public IEnumerable<T> GetMultiPagingJoin(Expression<Func<T, bool>> predicate, out int total, int index = 0, int size = 50, string[] includes = null)
        {
            int skipCount = index * size;
            IQueryable<T> _resetSet;

            if (includes != null && includes.Count() > 0)
            {
                var query = _entity.Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                _resetSet = predicate != null ? query.Where(predicate).AsQueryable() : query.AsQueryable();
            }
            else
            {
                _resetSet = predicate != null ? _entity.Where(predicate).AsQueryable() : _entity.AsQueryable();
            }

            _resetSet = skipCount == 0 ? _resetSet.Take(size) : _resetSet.Skip(skipCount).Take(size);
            total = _resetSet.Count();
            return _resetSet.AsQueryable();
        }

        public Task UpdateAsync(T entity)
        {
            _context.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        //public bool CheckContains(Expression<Func<T, bool>> predicate)
        //{
        //    return _entity.Count<T>(predicate) > 0;
        //}

        //public int Count(Expression<Func<T, bool>> where)
        //{
        //    return _entity.Count(where);
        //}

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



        public Task UpdateAsync(T entity)
        {
            _context.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }
    }
}

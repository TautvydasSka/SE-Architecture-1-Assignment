using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly BooksDbContext _dbContext;

        public Repository(BooksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (include != null) query = include(query);
            if (predicate != null) query = query.Where(predicate);

            return query.AsEnumerable();
        }

        public void Delete(T entity)
        {
            _dbContext.Remove(entity); 
        }

        public Task<T> GetFirstOrDefault(
            Expression<Func<T, bool>> predicate = null, 
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (include != null) query = include(query);
            if (predicate != null) query = query.Where(predicate);

            return query.FirstOrDefaultAsync();
        }

        public async Task Add(T entity)
        {
            await _dbContext.AddAsync(entity);
        }
    }
}

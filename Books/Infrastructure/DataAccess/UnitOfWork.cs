using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BooksDbContext _context;
        private readonly Dictionary<Type, object> _repositories;

        public UnitOfWork(BooksDbContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
        }

        public Task<int> CommitAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            var targetType = typeof(T);

            if (!_repositories.ContainsKey(targetType))
            {
                _repositories[targetType] = new Repository<T>(this._context);
            }

            return (IRepository<T>)this._repositories[targetType];
        }
    }
}

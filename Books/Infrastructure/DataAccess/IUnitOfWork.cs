using System;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class;
        Task<int> CommitAsync();
    }
}

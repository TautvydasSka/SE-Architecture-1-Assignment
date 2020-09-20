using Domain;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public interface IBooksService
    {
        IEnumerable<Book> Get();
        Task<OperationResult<Book>> Add(Book book);
        Task<OperationResult<Book>> Update(Book book);
        Task<OperationResult<Book>> Delete(string isbn);
    }
}

using Domain.Entities;
using System.Collections.Generic;

namespace Infrastructure.DataAccess
{
    public interface IBooksRepository
    {
        IEnumerable<Book> GetAll();
        void Add(Book book);
        void Update(Book book);
        void Delete(string isbn);
    }
}

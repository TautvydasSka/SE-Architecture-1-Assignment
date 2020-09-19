using Domain.Entities;
using System.Collections.Generic;

namespace Service
{
    public interface IBooksService
    {
        IEnumerable<Book> GetBooks();
        void Add(Book book);
        void Update(Book book);
        void Delete(string isbn);
    }
}

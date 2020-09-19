using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Infrastructure.DataAccess
{
    public class BooksRepository : IBooksRepository
    {
        private readonly List<Book> _books = new List<Book>
        {
            new Book
            {
                ISBN = "123",
                Author = "me",
                Title = "my book",
                PageCount = 456
            }
        };

        public IEnumerable<Book> GetAll()
        {
            return _books;
        }

        public void Delete(string isbn)
        {
            _books.RemoveAll(b => b.ISBN.Equals(isbn, StringComparison.InvariantCultureIgnoreCase));
        }

        public void Update(Book book)
        {
            Delete(book.ISBN);
            _books.Add(book);
        }

        public void Add(Book book)
        {
            _books.Add(book);
        }
    }
}

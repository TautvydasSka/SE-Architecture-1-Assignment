using Domain.Entities;
using Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class BooksService : IBooksService
    {
        private readonly IBooksRepository _booksRepository;

        public BooksService(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        public void Add(Book book)
        {
            var existingBooks = _booksRepository.GetAll();

            if (existingBooks.Any(eb => eb.ISBN.Equals(book.ISBN, StringComparison.InvariantCultureIgnoreCase)))
            {
                // bad request
            }

            _booksRepository.Add(book);
        }

        public void Delete(string isbn)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetBooks()
        {
            return _booksRepository.GetAll();
        }

        public void Update(Book book)
        {
            throw new NotImplementedException();
        }
    }
}

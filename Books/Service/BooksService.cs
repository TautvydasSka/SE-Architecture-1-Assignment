using Domain.Entities;
using Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class BooksService : IBooksService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BooksService(IUnitOfWork booksRepository)
        {
            _unitOfWork = booksRepository;
        }

        public void Add(Book book)
        {
            //var existingBooks = _unitOfWork.GetAll();

            //if (existingBooks.Any(eb => eb.ISBN.Equals(book.ISBN, StringComparison.InvariantCultureIgnoreCase)))
            //{
            //    // bad request
            //}

            //_unitOfWork.Add(book);

            throw new NotImplementedException();

        }

        public void Delete(string isbn)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetBooks()
        {
            throw new NotImplementedException();
        }

        public void Update(Book book)
        {
            throw new NotImplementedException();
        }
    }
}

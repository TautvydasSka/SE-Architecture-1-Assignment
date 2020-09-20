using Domain;
using Domain.Entities;
using Infrastructure.DataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public class BooksService : IBooksService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Book> _bookRepository;

        public BooksService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _bookRepository = _unitOfWork.GetRepository<Book>();
        }

        public async Task<OperationResult<Book>> Add(Book book)
        {
            var operationResult = new OperationResult<Book>();
            var existingBookWithSameIsbn = await _bookRepository.GetFirstOrDefault(b => b.ISBN.Equals(book.ISBN));

            if (existingBookWithSameIsbn != null)
            {
                operationResult.ValidationResult.PropertyValidations.Add(nameof(Book.ISBN), new List<string> { "A book with the same ISBN already exists!" });
                operationResult.Status = Status.ValidationError;
                return operationResult;
            }

            await _bookRepository.Add(book);
            await _unitOfWork.CommitAsync();

            return operationResult;
        }

        public async Task<OperationResult<Book>> Delete(string isbn)
        {
            var operationResult = new OperationResult<Book>();
            var existingBook = await _bookRepository.GetFirstOrDefault(b => b.ISBN.Equals(isbn));

            if (existingBook == null)
            {
                operationResult.Status = Status.NotFound;
                return operationResult;
            }

            _bookRepository.Delete(existingBook);
            await _unitOfWork.CommitAsync();

            return operationResult;
        }

        public IEnumerable<Book> Get()
        {
            return _bookRepository.Get();
        }

        public async Task<OperationResult<Book>> Update(Book book)
        {
            var operationResult = new OperationResult<Book>();
            var existingBook = await _bookRepository.GetFirstOrDefault(b => b.ISBN.Equals(book.ISBN));

            if (existingBook == null)
            {
                operationResult.Status = Status.NotFound;
                return operationResult;
            }

            existingBook.Author = book.Author;
            existingBook.Title = book.Title;
            existingBook.PageCount = book.PageCount;

            await _unitOfWork.CommitAsync();

            return operationResult;
        }
    }
}

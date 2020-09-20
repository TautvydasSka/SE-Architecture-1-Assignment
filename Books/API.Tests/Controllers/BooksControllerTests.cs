using API.Controllers;
using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using Service;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Tests
{
    public class BooksControllerTests
    {
        private BooksController _booksController;
        private IBooksService _booksService;

        [SetUp]
        public void Setup()
        {
            _booksService = Substitute.For<IBooksService>();
            _booksController = new BooksController(_booksService);
        }

        [Test]
        public void Get_ShouldReturnOkAndDataFromService()
        {
            // Arrange
            const string expectedIsbn = "9780132350884";
            const string expectedAuthor = "Robert C. Martin";
            const string expectedTitle = "Clean Code: A Handbook of Agile Software Craftsmanship";
            const int expectedPageCount = 464;

            var data = new List<Book>
            {
                new Book
                {
                    ISBN = expectedIsbn,
                    Author = expectedAuthor,
                    Title = expectedTitle,
                    PageCount = expectedPageCount
                }
            };

            _booksService.Get().Returns(data);

            // Act
            var result = _booksController.Get().Result as OkObjectResult;
            var resultData = result.Value as IEnumerable<Contract.Book>;

            // Assert
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.IsTrue(resultData.Any(b => b.ISBN.Equals(expectedIsbn)
                && b.Author.Equals(expectedAuthor)
                && b.Title.Equals(expectedTitle)
                && b.PageCount == expectedPageCount));
        }

        [Test]
        public async Task Post_NoValidationError_ReturnsCreatedStatusCode()
        {
            // Arrange
            var model = new Contract.Book
            {
                ISBN = "9780132350884",
                Author = "Robert C. Martin",
                Title = "Clean Code: A Handbook of Agile Software Craftsmanship",
                PageCount = 464
            };

            _booksService.Add(Arg.Any<Book>()).Returns(new OperationResult<Book> { Status = Status.Success });

            // Act
            var result = await _booksController.Post(model) as StatusCodeResult;

            // Assert
            Assert.AreEqual((int)HttpStatusCode.Created, result.StatusCode);
        }

        [Test]
        public async Task Post_HasValidationErrors_ReturnsBadRequestAndValidationError()
        {
            // Arrange
            var model = new Contract.Book
            {
                ISBN = "9780132350884",
                Author = "Robert C. Martin",
                Title = "Clean Code: A Handbook of Agile Software Craftsmanship",
                PageCount = 464
            };

            var validationResult = new ValidationResult();
            const string expectedValidationMessage = "A book with the same ISBN already exists!";
            validationResult.PropertyValidations.Add(nameof(Book.ISBN), new List<string> { expectedValidationMessage });

            _booksService.Add(Arg.Any<Book>()).Returns(new OperationResult<Book> { Status = Status.ValidationError, ValidationResult = validationResult });

            // Act
            var result = await _booksController.Post(model) as BadRequestObjectResult;
            var validation = (result.Value as IEnumerable<KeyValuePair<string, object>>).First();

            // Assert
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.StatusCode);
            Assert.AreEqual(nameof(Book.ISBN), validation.Key);
            Assert.AreEqual(expectedValidationMessage, (validation.Value as string[])[0]);
        }

        [Test]
        public async Task Put_NoValidationError_ReturnsOkStatusCode()
        {
            // Arrange
            var model = new Contract.Book
            {
                ISBN = "9780132350884",
                Author = "Robert C. Martin",
                Title = "Clean Code: A Handbook of Agile Software Craftsmanship",
                PageCount = 464
            };

            _booksService.Update(Arg.Any<Book>()).Returns(new OperationResult<Book> { Status = Status.Success });

            // Act
            var result = await _booksController.Put(model) as StatusCodeResult;

            // Assert
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Test]
        public async Task Put_HasValidationErrors_ReturnsBadRequestAndValidationError()
        {
            // Arrange
            var model = new Contract.Book
            {
                ISBN = "9780132350884",
                Author = "Robert C. Martin",
                Title = "Clean Code: A Handbook of Agile Software Craftsmanship",
                PageCount = 464
            };

            var validationResult = new ValidationResult();
            const string expectedValidationMessage = "A book with the same ISBN already exists!";
            validationResult.PropertyValidations.Add(nameof(Book.ISBN), new List<string> { expectedValidationMessage });

            _booksService.Update(Arg.Any<Book>()).Returns(new OperationResult<Book> { Status = Status.ValidationError, ValidationResult = validationResult });

            // Act
            var result = await _booksController.Put(model) as BadRequestObjectResult;
            var validation = (result.Value as IEnumerable<KeyValuePair<string, object>>).First();

            // Assert
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.StatusCode);
            Assert.AreEqual(nameof(Book.ISBN), validation.Key);
            Assert.AreEqual(expectedValidationMessage, (validation.Value as string[])[0]);
        }

        [Test]
        public async Task Put_BookNotFound_ReturnsNotFound()
        {
            // Arrange
            var model = new Contract.Book
            {
                ISBN = "9780132350884",
                Author = "Robert C. Martin",
                Title = "Clean Code: A Handbook of Agile Software Craftsmanship",
                PageCount = 464
            };

            _booksService.Update(Arg.Any<Book>()).Returns(new OperationResult<Book> { Status = Status.NotFound });

            // Act
            var result = await _booksController.Put(model) as StatusCodeResult;

            // Assert
            Assert.AreEqual((int)HttpStatusCode.NotFound, result.StatusCode);
        }

        [Test]
        public async Task Delete_SuccessfulStatusCode_ReturnsOkStatusCode()
        {
            // Arrange
            _booksService.Delete(Arg.Any<string>()).Returns(new OperationResult<Book> { Status = Status.Success });

            // Act
            var result = await _booksController.Delete("9780132350884") as StatusCodeResult;

            // Assert
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Test]
        public async Task Delete_BookNotFound_ReturnsNotFound()
        {
            // Arrange
            _booksService.Delete(Arg.Any<string>()).Returns(new OperationResult<Book> { Status = Status.NotFound });

            // Act
            var result = await _booksController.Delete("9780132350884") as StatusCodeResult;

            // Assert
            Assert.AreEqual((int)HttpStatusCode.NotFound, result.StatusCode);
        }
    }
}
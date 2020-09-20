using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Contract.Tests
{
    public class BookTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Isbn_WhenNull_NotValid()
        {
            // Arrange
            var model = new Book
            {
                ISBN = null,
                Author = "Robert C. Martin",
                Title = "Clean Code: A Handbook of Agile Software Craftsmanship",
                PageCount = 464
            };

            // Act
            var result = ValidateModel(model);

            // Assert
            Assert.IsTrue(result.Any(r => r.ErrorMessage.Contains("required")));
        }

        [Test]
        public void Isbn_TooShort_NotValid()
        {
            // Arrange
            var model = new Book
            {
                ISBN = "45345",
                Author = "Robert C. Martin",
                Title = "Clean Code: A Handbook of Agile Software Craftsmanship",
                PageCount = 464
            };

            // Act
            var result = ValidateModel(model);

            // Assert
            Assert.IsTrue(result.Any(r => r.ErrorMessage.Contains("minimum length")));
        }

        [Test]
        public void Author_WhenNull_NotValid()
        {
            // Arrange
            var model = new Book
            {
                ISBN = "9780132350884",
                Author = null,
                Title = "Clean Code: A Handbook of Agile Software Craftsmanship",
                PageCount = 464
            };

            // Act
            var result = ValidateModel(model);

            // Assert
            Assert.IsTrue(result.Any(r => r.ErrorMessage.Contains("required")));
        }

        [Test]
        public void Author_TooShort_NotValid()
        {
            // Arrange
            var model = new Book
            {
                ISBN = "9780132350884",
                Author = "R",
                Title = "Clean Code: A Handbook of Agile Software Craftsmanship",
                PageCount = 464
            };

            // Act
            var result = ValidateModel(model);

            // Assert
            Assert.IsTrue(result.Any(r => r.ErrorMessage.Contains("minimum length")));
        }

        [Test]
        public void Title_WhenNull_NotValid()
        {
            // Arrange
            var model = new Book
            {
                ISBN = "9780132350884",
                Author = "Robert C. Martin",
                Title = null,
                PageCount = 464
            };

            // Act
            var result = ValidateModel(model);

            // Assert
            Assert.IsTrue(result.Any(r => r.ErrorMessage.Contains("required")));
        }

        [Test]
        public void Title_TooShort_NotValid()
        {
            // Arrange
            var model = new Book
            {
                ISBN = "9780132350884",
                Author = "Robert C. Martin",
                Title = "C",
                PageCount = 464
            };

            // Act
            var result = ValidateModel(model);

            // Assert
            Assert.IsTrue(result.Any(r => r.ErrorMessage.Contains("minimum length")));
        }

        [Test]
        public void PageCount_When0_NotValid()
        {
            // Arrange
            var model = new Book
            {
                ISBN = "9780132350884",
                Author = "Robert C. Martin",
                Title = "Clean Code: A Handbook of Agile Software Craftsmanship",
                PageCount = 0
            };

            // Act
            var result = ValidateModel(model);

            // Assert
            Assert.IsTrue(result.Any(r => r.ErrorMessage.Contains("must be between 1")));
        }

        private static IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, null, null);

            Validator.TryValidateObject(model, validationContext, validationResults, true);

            return validationResults;
        }
    }
}

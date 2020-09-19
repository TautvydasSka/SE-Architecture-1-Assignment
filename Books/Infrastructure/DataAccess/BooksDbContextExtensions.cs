using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Infrastructure.DataAccess
{
    public static class BooksDbContextExtensions
    {
        public static ModelBuilder ConfigureBook(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasKey(b => b.ISBN);

            var data = new List<Book>
            {
                new Book
                {
                    ISBN = "9780132350884",
                    Author = "Robert C. Martin",
                    Title = "Clean Code: A Handbook of Agile Software Craftsmanship",
                    PageCount = 464
                },
                new Book
                {
                    ISBN = "0134494164",
                    Author = "Robert C. Martin",
                    Title = "Clean Architecture: A Craftsman's Guide to Software Structure and Design",
                    PageCount = 432
                },
                new Book
                {
                    ISBN = "0137081073",
                    Author = "Robert C. Martin",
                    Title = "The Clean Coder: A Code of Conduct for Professional Programmers",
                    PageCount = 242
                },
            };

            modelBuilder.Entity<Book>()
                .HasData(data);

            return modelBuilder;
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Contract
{
    public class Book
    {
        [Required]
        [MinLength(10)]
        public string ISBN { get; set; }

        [Required]
        [MinLength(2)]
        public string Author { get; set; }

        [Required]
        [MinLength(2)]
        public string Title { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int PageCount { get; set; }

        public Domain.Entities.Book ToDomainObject()
        {
            return new Domain.Entities.Book
            {
                ISBN = ISBN,
                Author = Author,
                Title = Title,
                PageCount = PageCount
            };
        }
    }
}

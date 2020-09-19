using System.Collections.Generic;
using System.Net;
using Contract;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _booksService;

        public BooksController(IBooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> Get()
        {
            return Ok(_booksService.GetBooks());
        }

        [HttpPost]
        public ActionResult Post([FromBody] Book book)
        {
            _booksService.Add(book.ToDomainObject());

            return StatusCode((int)HttpStatusCode.Created);
        }
    }
}

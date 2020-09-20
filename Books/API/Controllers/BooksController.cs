using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using API.ExtensionMethods;
using Contract;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace API.Controllers
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
            var data = _booksService.Get().Select(b => new Book
            {
                ISBN = b.ISBN,
                Author = b.Author,
                Title = b.Title,
                PageCount = b.PageCount
            });

            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Book book)
        {
            var response = await _booksService.Add(book.ToDomainObject());

            if (response.Status == Status.ValidationError)
            {
                return BadRequest(response.ValidationResult.ToModelStateDictionary());
            }

            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] Book book)
        {
            var response = await _booksService.Update(book.ToDomainObject());

            if (response.Status == Status.ValidationError)
            {
                return BadRequest(response.ValidationResult.ToModelStateDictionary());
            } 
            else if (response.Status == Status.NotFound)
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }

            return StatusCode((int)HttpStatusCode.OK);
        }

        [HttpDelete("{isbn}")]
        public async Task<ActionResult> Delete([FromRoute] string isbn)
        {
            var response = await _booksService.Delete(isbn);

            if (response.Status == Status.NotFound)
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }

            return StatusCode((int)HttpStatusCode.OK);
        }
    }
}

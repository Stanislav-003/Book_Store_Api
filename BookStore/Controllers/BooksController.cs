using BookStore.Api.Contracts;
using BookStore.Core.Abstractions;
using BookStore.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _booksService;

        public BooksController(IBooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BooksResponse>>> GetBooks()
        { 
            var books = await _booksService.GetAllBooks();

            var response = books.Select(b => new BooksResponse(b.Id, b.Title, b.Description, b.Price));

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateBook([FromBody]BooksRequest booksRequest)
        {
            var (book, error) = Book.Create(Guid.NewGuid(), booksRequest.title, booksRequest.description, booksRequest.price);

            if (!string.IsNullOrEmpty(error))
            { 
                return BadRequest(error);
            }

            await _booksService.CreateBook(book);

            return Ok(book.Id);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateBook(Guid id, [FromBody]BooksRequest booksRequest)
        {
            var bookId = await _booksService.UpdateBook(id, booksRequest.title, booksRequest.description, booksRequest.price);

            return Ok(bookId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteBook(Guid id)
        {
            var bookId = await _booksService.DeleteBook(id);

            return Ok(bookId);
        }
    }
}

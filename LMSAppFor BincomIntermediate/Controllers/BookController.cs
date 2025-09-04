using LMSAppFor_BincomIntermediate.Dtos;
using LMSAppFor_BincomIntermediate.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMSAppFor_BincomIntermediate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost("add-book")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddBook([FromBody] BookDto bookDto)
        {
            var response = await _bookService.AddBook(bookDto);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPut("update-book/{bookId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateBook([FromRoute] Guid bookId, [FromBody] BookDto bookDto)
        {
            var response = await _bookService.UpdateBook(bookId, bookDto);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpDelete("delete-book/{bookId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBook([FromRoute] Guid bookId)
        {
            var response = await _bookService.DeleteBook(bookId);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("get-all-books-grouped-by-author")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetAllBooksGroupedByAuthor([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var response = await _bookService.GetAllBooksGroupedByAuthor(pageNumber, pageSize);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("get-book-by-id/{bookId}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetBookById([FromRoute] Guid bookId)
        {
            var response = await _bookService.GetBookById(bookId);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("get-top-three-borrowed-books")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetMostTopThreeBorrowedBooks()
        {
            var response = await _bookService.GetMostTopThreeBorrowedBooks();
            return StatusCode((int)response.StatusCode, response);
        }
    }
}

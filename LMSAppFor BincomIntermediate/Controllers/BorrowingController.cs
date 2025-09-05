using LMSAppFor_BincomIntermediate.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMSAppFor_BincomIntermediate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowingController : ControllerBase
    {
        private readonly IBorrowingService _borrowingService;
        public BorrowingController(IBorrowingService borrowingService)
        {
            _borrowingService = borrowingService;
        }

        [HttpPost("borrow-book/{userId}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> BorrowBook(Guid userId, Guid bookId)
        {
            var response = await _borrowingService.BorrowBook(userId, bookId);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPost("return-book/{userId}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> ReturnBook(Guid userId, Guid bookId)
        {
            var response = await _borrowingService.ReturnBook(userId, bookId);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}

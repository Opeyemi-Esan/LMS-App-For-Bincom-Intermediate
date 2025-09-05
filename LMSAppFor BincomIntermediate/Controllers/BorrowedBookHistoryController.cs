using LMSAppFor_BincomIntermediate.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMSAppFor_BincomIntermediate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowedBookHistoryController : ControllerBase
    {
        private readonly IBorrowedBookHistoryService _borrowedBookHistoryService;
        public BorrowedBookHistoryController(IBorrowedBookHistoryService borrowedBookHistoryService)
        {
            _borrowedBookHistoryService = borrowedBookHistoryService;
        }


        [HttpGet("user-borrowed-history/{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserBorrowHistory(Guid userId)
        {
            var response = await _borrowedBookHistoryService.GetUserBorrowHistory(userId);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("book-borrowed-history/{bookId}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetBookBorrowHistory(Guid bookId)
        {
            var response = await _borrowedBookHistoryService.GetBookBorrowHistory(bookId);
            return StatusCode((int)response.StatusCode,response);
        }

        [HttpGet("active-borrow/{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetActiveBorrows([FromRoute] Guid userId)
        {
            var response = await _borrowedBookHistoryService.GetActiveBorrows(userId);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("user-borrowed-history")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetAllActiveBorrows()
        {
            var response = await _borrowedBookHistoryService.GetAllActiveBorrows();
            return StatusCode((int)response.StatusCode, response);
        }
    }
}

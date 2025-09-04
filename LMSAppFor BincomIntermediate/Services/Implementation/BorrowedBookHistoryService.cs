using LMSAppFor_BincomIntermediate.Data;
using LMSAppFor_BincomIntermediate.Models;
using LMSAppFor_BincomIntermediate.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace LMSAppFor_BincomIntermediate.Services.Implementation
{
    public class BorrowedBookHistoryService : IBorrowedBookHistoryService
    {
        private readonly LibraryDbContext _context;
        private readonly ILogger<BorrowedBookHistoryService> _logger;
        public BorrowedBookHistoryService(LibraryDbContext context, ILogger<BorrowedBookHistoryService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<BookResponse<List<BorrowedBookHistory>>> GetActiveBorrows(Guid userId)
        {
            try
            {
                // Fetch active borrows for the specified user
                var activeBorrows = await _context.BorrowedBookHistories
                    .Where(b => b.UserId == userId && !b.IsReturned)
                    .Include(b => b.Book)
                    .OrderByDescending(h => h.BorrowDate)
                    .AsNoTracking()
                    .ToListAsync();

                return new BookResponse<List<BorrowedBookHistory>>
                {
                    IsSuccess = true,
                    Data = activeBorrows,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving active borrows.");
                return new BookResponse<List<BorrowedBookHistory>>
                {
                    IsSuccess = false,
                    Message = "An error occurred while retrieving active borrows.",
                    Data = null,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<BookResponse<List<BorrowedBookHistory>>> GetAllActiveBorrows()
        {
            try
            {
                // Fetch all active borrows
                var activeBorrows = await _context.BorrowedBookHistories
                    .Where(b => !b.IsReturned)
                    .Include(b => b.Book)
                    .Include(b => b.User)
                    .OrderByDescending(h => h.BorrowDate)
                    .AsNoTracking()
                    .ToListAsync();
                return new BookResponse<List<BorrowedBookHistory>>
                {
                    IsSuccess = true,
                    Data = activeBorrows,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all active borrows.");
                return new BookResponse<List<BorrowedBookHistory>>
                {
                    IsSuccess = false,
                    Message = "An error occurred while retrieving all active borrows.",
                    Data = null,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<BookResponse<List<BorrowedBookHistory>>> GetBookBorrowHistory(Guid bookId)
        {
            try
            {
                // Fetch complete borrow history for the specified book
                var borrowHistory = await _context.BorrowedBookHistories
                    .Where(b => b.BookId == bookId)
                    .Include(b => b.User)
                    .OrderByDescending(h => h.BorrowDate)
                    .AsNoTracking()
                    .ToListAsync();

                return new BookResponse<List<BorrowedBookHistory>>
                {
                    IsSuccess = true,
                    Data = borrowHistory,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving book borrow history.");
                return new BookResponse<List<BorrowedBookHistory>>
                {
                    IsSuccess = false,
                    Message = "An error occurred while retrieving book borrow history.",
                    Data = null,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<BookResponse<List<BorrowedBookHistory>>> GetUserBorrowHistory(Guid userId)
        {
            try
            {
                // Fetch complete borrow history for the specified user
                var borrowHistory = await _context.BorrowedBookHistories
                    .Where(b => b.UserId == userId)
                    .Include(b => b.Book)
                    .OrderByDescending(h => h.BorrowDate)
                    .AsNoTracking()
                    .ToListAsync();

                return new BookResponse<List<BorrowedBookHistory>>
                {
                    IsSuccess = true,
                    Data = borrowHistory,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving user borrow history.");
                return new BookResponse<List<BorrowedBookHistory>>
                {
                    IsSuccess = false,
                    Message = "An error occurred while retrieving user borrow history.",
                    Data = null,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }
    }
}

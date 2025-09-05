using LMSAppFor_BincomIntermediate.Data;
using LMSAppFor_BincomIntermediate.Dtos;
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
        public async Task<Response<List<BorrowedBookHistoryDto>>> GetActiveBorrows(Guid userId)
        {
            try
            {
                // Fetch active borrows for the specified user
                var activeBorrows = await _context.BorrowedBookHistories
                    .Where(b => b.UserId == userId && !b.IsReturned)
                    .Select(b => new BorrowedBookHistoryDto
                    {
                        UserId = b.UserId,
                        FirstName = b.User.FirstName,
                        LastName = b.User.LastName,
                        BookId = b.BookId,
                        BookTitle = b.Book.Title,
                        BorrowDate = b.BorrowDate,
                        ReturnDate = b.IsReturned ? b.ReturnDate : null,
                        IsReturned = b.IsReturned
                    })
                    .OrderByDescending(h => h.BorrowDate)
                    .AsNoTracking()
                    .ToListAsync();

                return new Response<List<BorrowedBookHistoryDto>>
                {
                    IsSuccess = true,
                    Data = activeBorrows,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving active borrows.");
                return new Response<List<BorrowedBookHistoryDto>>
                {
                    IsSuccess = false,
                    Message = "An error occurred while retrieving active borrows.",
                    Data = null,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<Response<List<BorrowedBookHistoryDto>>> GetAllActiveBorrows()
        {
            try
            {
                // Fetch all active borrows
                var activeBorrows = await _context.BorrowedBookHistories
                    .Where(b => !b.IsReturned)
                    .Select(b => new BorrowedBookHistoryDto
                    {
                        UserId = b.UserId,
                        FirstName = b.User.FirstName,
                        LastName = b.User.LastName,
                        BookId = b.BookId,
                        BookTitle = b.Book.Title,
                        BorrowDate = b.BorrowDate,
                    })
                    .OrderByDescending(h => h.BorrowDate)
                    .AsNoTracking()
                    .ToListAsync();
                return new Response<List<BorrowedBookHistoryDto>>
                {
                    IsSuccess = true,
                    Data = activeBorrows,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all active borrows.");
                return new Response<List<BorrowedBookHistoryDto>>
                {
                    IsSuccess = false,
                    Message = "An error occurred while retrieving all active borrows.",
                    Data = null,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<Response<List<BorrowedBookHistoryDto>>> GetBookBorrowHistory(Guid bookId)
        {
            try
            {
                // Fetch complete borrow history for the specified book
                var borrowHistory = await _context.BorrowedBookHistories
                    .Where(b => b.BookId == bookId)
                    .Select(b => new BorrowedBookHistoryDto
                    {
                        BookId = b.BookId,
                        BookTitle = b.Book.Title,
                        BorrowDate = b.BorrowDate,
                        ReturnDate = b.IsReturned ? b.ReturnDate : null,
                        IsReturned = b.IsReturned
                    })
                    .OrderByDescending(h => h.BorrowDate)
                    .AsNoTracking()
                    .ToListAsync();

                return new Response<List<BorrowedBookHistoryDto>>
                {
                    IsSuccess = true,
                    Data = borrowHistory,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving book borrow history.");
                return new Response<List<BorrowedBookHistoryDto>>
                {
                    IsSuccess = false,
                    Message = "An error occurred while retrieving book borrow history.",
                    Data = null,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<Response<List<BorrowedBookHistoryDto>>> GetUserBorrowHistory(Guid userId)
        {
            try
            {
                // Fetch complete borrow history for the specified user
                var borrowHistory = await _context.BorrowedBookHistories
                    .Where(b => b.UserId == userId)
                    .Select(b => new BorrowedBookHistoryDto
                    {
                        UserId = b.UserId,
                        FirstName = b.User.FirstName,
                        LastName = b.User.LastName,
                        BorrowDate = b.BorrowDate,
                        ReturnDate = b.IsReturned ? b.ReturnDate : null,
                        IsReturned = b.IsReturned
                    })
                    .OrderByDescending(h => h.BorrowDate)
                    .AsNoTracking()
                    .ToListAsync();

                return new Response<List<BorrowedBookHistoryDto>>
                {
                    IsSuccess = true,
                    Data = borrowHistory,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving user borrow history.");
                return new Response<List<BorrowedBookHistoryDto>>
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

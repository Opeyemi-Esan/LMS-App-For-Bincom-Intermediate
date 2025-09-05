using LMSAppFor_BincomIntermediate.Data;
using LMSAppFor_BincomIntermediate.Models;
using LMSAppFor_BincomIntermediate.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace LMSAppFor_BincomIntermediate.Services.Implementation
{
    public class BorrowingService : IBorrowingService
    {
        private readonly LibraryDbContext _context;
        private readonly ILogger<BorrowingService> _logger;
        public BorrowingService(LibraryDbContext context, ILogger<BorrowingService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Response<string>> BorrowBook(Guid userId, Guid bookId)
        {
            try
            {
                var book = await _context.Books.FindAsync(bookId);
                if (book == null || book.AvailableCopies <= 0)
                {
                    return new Response<string>
                    {
                        IsSuccess = false,
                        Message = "Book not available for borrowing.",
                        StatusCode = HttpStatusCode.BadRequest
                    };
                }

                var user = await _context.LibraryUsers
                    .Include(u => u.BorrowHistory)
                    .FirstOrDefaultAsync(u => u.Id == userId);

                if (user == null)
                {
                    return new Response<string>
                    {
                        IsSuccess = false,
                        Message = "User not found.",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                book.AvailableCopies--;

                var borrowHistory = new BorrowedBookHistory
                {
                    BookId = bookId,
                    UserId = userId,
                    BorrowDate = DateTime.UtcNow,
                    IsReturned = false
                };

                // add to user’s navigation property
                user.BorrowHistory ??= new List<BorrowedBookHistory>();
                user.BorrowHistory.Add(borrowHistory);

                await _context.SaveChangesAsync();

                return new Response<string>
                {
                    IsSuccess = true,
                    Message = "Book borrowed successfully.",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured");
                return new Response<string>
                {
                    IsSuccess = false,
                    Message = "An error occured",
                    StatusCode = HttpStatusCode.InternalServerError,
                };
            }
        }


        public async Task<Response<string>> ReturnBook(Guid userId, Guid bookId)
        {
            try
            {
                // Find the most recent borrow record for this user and book that hasn't been returned yet
                var book = await _context.Books.FindAsync(bookId);
                var borrowHistory = await _context.BorrowedBookHistories.Where(x => x.UserId == userId && x.BookId == bookId && !x.IsReturned)
                    .OrderByDescending(o => o.BorrowDate)
                    .FirstOrDefaultAsync();

                if (borrowHistory == null || book == null)
                {
                    return new Response<string>
                    {
                        IsSuccess = false,
                        Message = "No active borrow record found.",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                // Update the book's available copies
                if (book.AvailableCopies < book.TotalCopies)
                    book.AvailableCopies++;

                // Mark the borrow record as returned
                borrowHistory.IsReturned = true;
                borrowHistory.BorrowDate = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                return new Response<string>
                {
                    IsSuccess = true,
                    Message = "Book returned successfully.",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured");
                return new Response<string>
                {
                    IsSuccess = false,
                    Message = "An error occured",
                    StatusCode = HttpStatusCode.InternalServerError,
                };
            }
        }
    }
}

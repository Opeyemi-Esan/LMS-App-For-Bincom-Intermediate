using LMSAppFor_BincomIntermediate.Data;
using LMSAppFor_BincomIntermediate.Dtos;
using LMSAppFor_BincomIntermediate.Models;
using LMSAppFor_BincomIntermediate.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace LMSAppFor_BincomIntermediate.Services.Implementation
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext _context;
        private readonly ILogger<BookService> _logger;
        public BookService(LibraryDbContext context, ILogger<BookService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<BookResponse<string>> AddBook(BookDto bookDto)
        {
            try
            {
                // check if the book with the same title and author already exists
                var existingBook = await _context.Books
                    .FirstOrDefaultAsync(b => b.Title.ToLower() == bookDto.Title.ToLower() && b.Author.ToLower() == bookDto.Author.ToLower());

                if (existingBook != null)
                {
                    return new BookResponse<string>
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = "A book with the same title and author already exists.",
                        StatusCode = HttpStatusCode.Conflict
                    };
                }

                var newBook = new Book
                {
                    Title = bookDto.Title,
                    Author = bookDto.Author,
                    ISBN = bookDto.ISBN,
                    TotalCopies = bookDto.TotalCopies,
                    AvailableCopies = bookDto.TotalCopies 
                };

                await _context.Books.AddAsync(newBook);
                await _context.SaveChangesAsync();
                return new BookResponse<string>
                {
                    Data = "Book added successfully.",
                    IsSuccess = true,
                    Message = "Book added successfully.",
                    StatusCode = HttpStatusCode.Created
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new book.");
                return new BookResponse<string>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "An error occurred while adding a new book",
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<BookResponse<string>> DeleteBook(Guid bookId)
        {
            try
            {
                var book = await _context.Books.FindAsync(bookId);
                if (book == null)
                {
                    return new BookResponse<string>
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = "Book not found.",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
                return new BookResponse<string>
                {
                    Data = null,
                    IsSuccess = true,
                    Message = "Book deleted successfully.",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the book.");
                return new BookResponse<string>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "An error occurred while deleting the book.",
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<BookResponse<List<AuthorGroupedBooksDto>>> GetAllBooksGroupedByAuthor(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var groupedBooks = await _context.Books
                    .AsNoTracking()
                    .GroupBy(b => b.Author)
                    .Select(g => new AuthorGroupedBooksDto
                    {
                        Author = g.Key,
                        Books = g.ToList()
                    })
                    .OrderBy(g => g.Author)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
                return new BookResponse<List<AuthorGroupedBooksDto>>
                {
                    Data = groupedBooks,
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving books.");
                return new BookResponse<List<AuthorGroupedBooksDto>>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "An error occurred while retrieving books.",
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<BookResponse<Book>> GetBookById(Guid bookId)
        {
            try
            {
                var book = await _context.Books.AsNoTracking().FirstOrDefaultAsync(b => b.Id == bookId);
                if (book == null)
                {
                    return new BookResponse<Book>
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = "Book not found.",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                return new BookResponse<Book>
                {
                    Data = book,
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the book.");
                return new BookResponse<Book>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "An error occurred while retrieving the book.",
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<BookResponse<List<TopBorrowedBookDto>>> GetMostTopThreeBorrowedBooks()
        {
            try
            {
                // Group by BookId, count the occurrences, order by count descending, take top 3, and join with Books to get book details
                var mostBorrowedBooks = await _context.BorrowedBookHistories
                    .AsNoTracking()
                    .GroupBy(b => b.BookId)
                    .Select(g => new
                    {
                        BookId = g.Key,
                        BorrowCount = g.Count()
                    })
                    .OrderByDescending(g => g.BorrowCount)
                    .Take(3)
                    .Join(_context.Books,
                          borrow => borrow.BookId,
                          book => book.Id,
                          (borrow, book) => new TopBorrowedBookDto
                          {
                              Title = book.Title,
                              Author = book.Author,
                              BorrowCount = borrow.BorrowCount

                          })
                    .ToListAsync();

                return new BookResponse<List<TopBorrowedBookDto>>
                {
                    Data = mostBorrowedBooks,
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the most borrowed books.");
                return new BookResponse<List<TopBorrowedBookDto>>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "An error occurred while retrieving the most borrowed books.",
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<BookResponse<string>> UpdateBook(Guid bookId, BookDto bookDto)
        {
            try
            {
                var book = await _context.Books.FindAsync();
                if (book == null)
                {
                    return new BookResponse<string>
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = "Book not found.",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                book.Title = bookDto.Title;
                book.Author = bookDto.Author;
                book.ISBN = bookDto.ISBN;
                book.TotalCopies = bookDto.TotalCopies;

                await _context.SaveChangesAsync();

                return new BookResponse<string>
                {
                    Data = null,
                    IsSuccess = true,
                    Message = "Book updated successfully",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while Updating the book.");
                return new BookResponse<string>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "An error occurred while Updating the book.",
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }
    }
}

using LMSAppFor_BincomIntermediate.Data;
using LMSAppFor_BincomIntermediate.Dtos;
using LMSAppFor_BincomIntermediate.Models;

namespace LMSAppFor_BincomIntermediate.Services.Interface
{
    public interface IBookService
    {
        Task<BookResponse<string>> AddBook(BookDto bookDto);
        Task<BookResponse<string>> UpdateBook(Guid bookId, BookDto bookDto);
        Task<BookResponse<string>> DeleteBook(Guid bookId);
        Task<BookResponse<List<AuthorGroupedBooksDto>>> GetAllBooksGroupedByAuthor(int pageNumber = 1, int pageSize = 10);
        Task<BookResponse<Book>> GetBookById(Guid bookId);
        Task<BookResponse<List<TopBorrowedBookDto>>> GetMostTopThreeBorrowedBooks();

    }
}

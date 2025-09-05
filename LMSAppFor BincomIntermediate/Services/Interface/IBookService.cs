using LMSAppFor_BincomIntermediate.Data;
using LMSAppFor_BincomIntermediate.Dtos;
using LMSAppFor_BincomIntermediate.Models;

namespace LMSAppFor_BincomIntermediate.Services.Interface
{
    public interface IBookService
    {
        Task<Response<string>> AddBook(BookDto bookDto);
        Task<Response<string>> UpdateBook(Guid bookId, BookDto bookDto);
        Task<Response<string>> DeleteBook(Guid bookId);
        Task<GetResponse<List<AuthorGroupedBooksDto>>> GetAllBooksGroupedByAuthor(int pageNumber = 1, int pageSize = 10);
        Task<Response<Book>> GetBookById(Guid bookId);
        Task<Response<List<TopBorrowedBookDto>>> GetMostTopThreeBorrowedBooks();

    }
}

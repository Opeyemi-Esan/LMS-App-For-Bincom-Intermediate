using LMSAppFor_BincomIntermediate.Data;

namespace LMSAppFor_BincomIntermediate.Services.Interface
{
    public interface IBorrowingService
    {
        Task<BookResponse<string>> BorrowBook(Guid userId, Guid bookId);
        Task<BookResponse<string>> ReturnBook(Guid userId, Guid bookId);
    }
}

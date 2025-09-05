using LMSAppFor_BincomIntermediate.Data;

namespace LMSAppFor_BincomIntermediate.Services.Interface
{
    public interface IBorrowingService
    {
        Task<Response<string>> BorrowBook(Guid userId, Guid bookId);
        Task<Response<string>> ReturnBook(Guid userId, Guid bookId);
    }
}

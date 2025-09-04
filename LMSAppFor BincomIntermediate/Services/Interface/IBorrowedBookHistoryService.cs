using LMSAppFor_BincomIntermediate.Data;
using LMSAppFor_BincomIntermediate.Models;

namespace LMSAppFor_BincomIntermediate.Services.Interface
{
    public interface IBorrowedBookHistoryService
    {
        Task<BookResponse<List<BorrowedBookHistory>>> GetUserBorrowHistory(Guid userId);
        Task<BookResponse<List<BorrowedBookHistory>>> GetBookBorrowHistory(Guid bookId);
        Task<BookResponse<List<BorrowedBookHistory>>> GetActiveBorrows(Guid userId);
        Task<BookResponse<List<BorrowedBookHistory>>> GetAllActiveBorrows();
    }
}

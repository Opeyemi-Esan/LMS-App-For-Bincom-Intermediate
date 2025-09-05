using LMSAppFor_BincomIntermediate.Data;
using LMSAppFor_BincomIntermediate.Dtos;
using LMSAppFor_BincomIntermediate.Models;

namespace LMSAppFor_BincomIntermediate.Services.Interface
{
    public interface IBorrowedBookHistoryService
    {
        Task<Response<List<BorrowedBookHistoryDto>>> GetUserBorrowHistory(Guid userId);
        Task<Response<List<BorrowedBookHistoryDto>>> GetBookBorrowHistory(Guid bookId);
        Task<Response<List<BorrowedBookHistoryDto>>> GetActiveBorrows(Guid userId);
        Task<Response<List<BorrowedBookHistoryDto>>> GetAllActiveBorrows();
    }
}

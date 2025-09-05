using LMSAppFor_BincomIntermediate.Data;
using LMSAppFor_BincomIntermediate.Dtos;

namespace LMSAppFor_BincomIntermediate.Services.Interface
{
    public interface IExternalBookService
    {
        Task<Response<BookDto>> GetBookDetails(string title);
    }
}

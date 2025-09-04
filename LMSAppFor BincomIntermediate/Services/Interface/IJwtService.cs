using LMSAppFor_BincomIntermediate.Models;

namespace LMSAppFor_BincomIntermediate.Services.Interface
{
    public interface IJwtService
    {
        Task<string> GenerateToken(LibraryUser user);
    }
}

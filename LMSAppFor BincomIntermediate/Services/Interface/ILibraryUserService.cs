using LMSAppFor_BincomIntermediate.Data;
using LMSAppFor_BincomIntermediate.Dtos;
using LMSAppFor_BincomIntermediate.Models;

namespace LMSAppFor_BincomIntermediate.Services.Interface
{
    public interface ILibraryUserService
    {
        Task<Response<string>>RegisterUser(LibraryUserRegDto libraryUserRegDto);
        Task<Response<string>> LoginUser(LoginDto loginDto);
        Task<GetResponse<List<LibraryUser>>> GetUsers(int pageNumber = 1, int pageSize = 10);
        Task<Response<LibraryUser>> GetUserById(Guid UserId);
        Task<Response<string>> UpdateUser(Guid userId, UpdateUserDto updateUserDto);
        Task<Response<string>> DeleteUser(Guid userId, string password);
    }
}

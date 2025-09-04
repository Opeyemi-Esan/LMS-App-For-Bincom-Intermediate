using LMSAppFor_BincomIntermediate.Dtos;
using LMSAppFor_BincomIntermediate.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMSAppFor_BincomIntermediate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryUserController : ControllerBase
    {
        private readonly ILibraryUserService _libraryUserService;
        public LibraryUserController(ILibraryUserService libraryUserService)
        {
            _libraryUserService = libraryUserService;
        }

        [HttpGet("Geta_all_users")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _libraryUserService.GetUsers(pageNumber, pageSize);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("Get_user_by_id/{userId}")]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            var response = await _libraryUserService.GetUserById(userId);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPost("Register_user")]
        public async Task<IActionResult> RegisterUser([FromBody] LibraryUserRegDto libraryUserRegDto)
        {
            var response = await _libraryUserService.RegisterUser(libraryUserRegDto);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPost("Login_user")]
        public async Task<IActionResult> LoginUser([FromBody] LoginDto loginDto)
        {
            var response = await _libraryUserService.LoginUser(loginDto);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPut("Update_user/{userId}")]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> UpdateUser(Guid userId, [FromBody] UpdateUserDto updateUserDto)
        {
            var response = await _libraryUserService.UpdateUser(userId, updateUserDto);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpDelete("Delete_user/{userId}")]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> DeleteUser(Guid userId, [FromQuery] string password)
        {
            var response = await _libraryUserService.DeleteUser(userId, password);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}

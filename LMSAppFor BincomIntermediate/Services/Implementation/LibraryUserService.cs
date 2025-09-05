using LMSAppFor_BincomIntermediate.Data;
using LMSAppFor_BincomIntermediate.Dtos;
using LMSAppFor_BincomIntermediate.Models;
using LMSAppFor_BincomIntermediate.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using System.Net;
using BCryptNet = BCrypt.Net;

namespace LMSAppFor_BincomIntermediate.Services.Implementation
{
    public class LibraryUserService : ILibraryUserService
    {
        private readonly LibraryDbContext _context;
        private readonly IJwtService _jwtService;
        private readonly ILogger<LibraryUserService> _logger;
        public LibraryUserService(LibraryDbContext context, IJwtService jwtService, ILogger<LibraryUserService> logger) 
        { 
            _context = context;
            _jwtService = jwtService;
            _logger = logger;
        }

        public async Task<GetResponse<List<LibraryUser>>> GetUsers(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                // Validate pageNumber and pageSize
                if (pageNumber <= 0) pageNumber = 1;
                if (pageSize <= 0) pageSize = 10;

                var totalRecords = await _context.LibraryUsers.CountAsync();

                var users = await _context.LibraryUsers
                    .AsNoTracking()
                    .Where(u => !u.IsDeleted)
                    .Skip((pageNumber - 1) * pageSize)  
                    .Take(pageSize)                     
                    .ToListAsync();

                if(users == null)
                {
                    return new GetResponse<List<LibraryUser>>
                    {
                        IsSuccess = false,
                        StatusCode = HttpStatusCode.NotFound,
                        Message = "No user found, add a user",
                        Data = null
                    };
                }

                return new GetResponse<List<LibraryUser>>
                {
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK,
                    Data = users,
                    Message = $"Page {pageNumber} of {Math.Ceiling((double)totalRecords / pageSize)}",
                    TotalCount = totalRecords,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving users");
                return new GetResponse<List<LibraryUser>>
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "An error occurred while retrieving users.",
                    Data = null
                };
            }
        }

        public async Task<Response<LibraryUser>> GetUserById(Guid userId)
        {
            try
            {
                var user = await _context.LibraryUsers.FirstOrDefaultAsync(u => u.Id == userId && !u.IsDeleted);
                if (user == null)
                {
                    return new Response<LibraryUser>
                    {
                        IsSuccess = false,
                        StatusCode = HttpStatusCode.NotFound,
                        Message = "User not found.",
                        Data = null
                    };
                }
                return new Response<LibraryUser>
                {
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK,
                    Message = "User retrieved successfully.",
                    Data = user
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user by ID");
                return new Response<LibraryUser>
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "An error occurred while retrieving the user.",
                    Data = null
                };
            }
        }

        public async Task<Response<string>> RegisterUser(LibraryUserRegDto libraryUserRegDto)
        {
            try
            {
                var user = await _context.LibraryUsers.FirstOrDefaultAsync(u => u.Email == libraryUserRegDto.Email);
                if (user != null)
                {
                    return new Response<string>
                    {
                        IsSuccess = false,
                        StatusCode = HttpStatusCode.BadRequest,
                        Message = "User with this email already exists.",
                        Data = null
                    };
                }

                var newUser = new LibraryUser
                {
                    FirstName = libraryUserRegDto.FirstName,
                    LastName = libraryUserRegDto.LastName,
                    Email = libraryUserRegDto.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(libraryUserRegDto.Password),
                    Role = libraryUserRegDto.Role
                    
                };  
                await _context.LibraryUsers.AddAsync(newUser);
                await _context.SaveChangesAsync();
                return new Response<string>
                {
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.Created,
                    Message = "User registered successfully.",
                    Data = null
                };
            }
            catch (Exception ex) 
            { 
                _logger.LogError(ex, "Error registering user");
                return new Response<string>
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "An error occurred while registering the user.",
                    Data = null
                };
            }
        }

        public async Task<Response<string>> LoginUser(LoginDto loginDto)
        {
            try
            {
                // Check if user exists
                var user = await _context.LibraryUsers.FirstOrDefaultAsync(u => u.Email == loginDto.Email && !u.IsDeleted);

                // If user does not exist or password is incorrect
                if (user == null || !BCryptNet.BCrypt.Verify(loginDto.Password, user.PasswordHash))
                {
                    return new Response<string>
                    {
                        IsSuccess = false,
                        StatusCode = HttpStatusCode.Unauthorized,
                        Message = "Invalid email or password.",
                        Data = null
                    };
                }

                // Generate JWT token
                var token = await _jwtService.GenerateToken(user);
                return new Response<string>
                {
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK,
                    Message = "Login successful.",
                    Data = token
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error logging in user");
                return new Response<string>
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "An error occurred while logging in.",
                    Data = null
                };
            }   
        }

        public async Task<Response<string>> UpdateUser(Guid userId, UpdateUserDto updateUserDto)
        {
            try
            {
                // Check if user exists
                var user = await _context.LibraryUsers.FirstOrDefaultAsync(u => u.Id == userId && !u.IsDeleted);

                // If user does not exist
                if (user == null)
                {
                    return new Response<string>
                    {
                        IsSuccess = false,
                        StatusCode = HttpStatusCode.NotFound,
                        Message = "User not found.",
                        Data = null
                    };
                }

                // Update user details
                user.FirstName = updateUserDto.FirstName;
                user.LastName = updateUserDto.LastName;
                user.Email = updateUserDto.Email;
                user.Role = updateUserDto.Role;

                _context.LibraryUsers.Update(user);
                await _context.SaveChangesAsync();
    
                 return new Response<string>
                 {
                      IsSuccess = true,
                      StatusCode = HttpStatusCode.OK,
                      Message = "User updated successfully.",
                      Data = null
                 };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user");
                return new Response<string>
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "An error occurred while updating the user.",
                    Data = null
                };
            }
        }

        public async Task<Response<string>> DeleteUser(Guid userId, string password)
        {
            try
            {
                // Check if user exists
                var user = await _context.LibraryUsers.FirstOrDefaultAsync(u => u.Id == userId && !u.IsDeleted);
                // If user does not exist
                if (user == null)
                {
                    return new Response<string>
                    {
                        IsSuccess = false,
                        StatusCode = HttpStatusCode.NotFound,
                        Message = "User not found.",
                        Data = null
                    };
                }

                // Verify password
                if (!BCryptNet.BCrypt.Verify(password, user.PasswordHash))
                {
                    return new Response<string>
                    {
                        IsSuccess = false,
                        StatusCode = HttpStatusCode.Unauthorized,
                        Message = "You cannot delete this user your password is incorrect",
                        Data = null
                    };
                }

                // Soft delete the user instead of removing from database. Data can be useful in future.
                user.IsDeleted = true;
                await _context.SaveChangesAsync();

                return new Response<string>
                {
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK,
                    Message = "User deleted successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user");
                return new Response<string>
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "An error occurred while deleting the user.",
                    Data = null
                };

            }
        }
    }
}

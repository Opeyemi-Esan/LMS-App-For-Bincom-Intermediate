using LMSAppFor_BincomIntermediate.Data;
using LMSAppFor_BincomIntermediate.Dtos;
using LMSAppFor_BincomIntermediate.Services.Interface;
using System.Net;

namespace LMSAppFor_BincomIntermediate.Services.Implementation
{
    public class ExternalBookService : IExternalBookService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ExternalBookService> _logger;
        public ExternalBookService(HttpClient httpClient, ILogger<ExternalBookService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        public async Task<BookResponse<BookDto>> GetBookDetails(string title)
        {
            try
            {
                var apiurl = $"https://openlibrary.org/api/books?bibkeys=ISBN:{title}&format=json&jscmd=data";
                var response = await _httpClient.GetAsync(apiurl);

                if (!response.IsSuccessStatusCode)
                {
                    return new BookResponse<BookDto>
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = "Failed to fetch book details from OpenLibrary API.",
                        StatusCode = response.StatusCode
                    };
                }

                var content = await response.Content.ReadFromJsonAsync<Dictionary<string, LibraryBookDto>>();

                if (content == null || !content.ContainsKey($"ISBN:{title}"))
                {
                    return new BookResponse<BookDto>
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = "Book not found.",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                var bookData = content[$"ISBN:{title}"];

                var dto = new BookDto
                {
                    Title = bookData.Title,
                    Author = bookData.Authors?.FirstOrDefault()?.Name ?? "Unknown",
                    TotalCopies = 0 
                };

                return new BookResponse<BookDto>
                {
                    Data = dto,
                    IsSuccess = true,
                    Message = "Book details fetched successfully.",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                {
                    _logger.LogError(ex, "An error occured while fetching book from exteranl API");
                    return new BookResponse<BookDto>
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = "An error occured while fetching book from exteranl API",
                        StatusCode = HttpStatusCode.OK,
                    };
                }
            }
        }
    }
}

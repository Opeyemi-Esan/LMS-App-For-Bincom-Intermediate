using LMSAppFor_BincomIntermediate.Data;
using LMSAppFor_BincomIntermediate.Dtos;
using LMSAppFor_BincomIntermediate.Helper;
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

        public async Task<Response<BookDto>> GetBookDetails(string title)
        {
            try
            {
                var apiUrl = $"https://openlibrary.org/search.json?title={Uri.EscapeDataString(title)}";
                var response = await _httpClient.GetAsync(apiUrl);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response<BookDto>
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = "Failed to fetch book details from OpenLibrary API.",
                        StatusCode = response.StatusCode
                    };
                }

                var searchResult = await response.Content.ReadFromJsonAsync<OpenLibrarySearchResponse>();

                if (searchResult == null || searchResult.Docs == null || !searchResult.Docs.Any())
                {
                    return new Response<BookDto>
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = "Book not found.",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                var firstBook = searchResult.Docs.First();

                var dto = new BookDto
                {
                    Title = firstBook.Title,
                    Author = firstBook.Author_Name?.FirstOrDefault() ?? "Unknown",
                    TotalCopies = 0
                };

                return new Response<BookDto>
                {
                    Data = dto,
                    IsSuccess = true,
                    Message = "Book details fetched successfully.",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching book from external API");
                return new Response<BookDto>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "An error occurred while fetching book from external API",
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }
    }
}

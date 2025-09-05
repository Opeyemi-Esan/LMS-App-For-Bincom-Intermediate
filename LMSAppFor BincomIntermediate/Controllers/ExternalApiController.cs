using LMSAppFor_BincomIntermediate.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMSAppFor_BincomIntermediate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExternalApiController : ControllerBase
    {
        private readonly IExternalBookService _externalBookService;
        public ExternalApiController(IExternalBookService externalBookService)
        {
            _externalBookService = externalBookService;
        }

        [HttpGet("external-book-details/{title}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetBookDetails(string title)
        {
            var response = await _externalBookService.GetBookDetails(title);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}

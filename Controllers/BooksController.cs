using Microsoft.AspNetCore.Mvc;
using MongoDbPoc.Models;
using MongoDbPoc.Services.Interfaces;

namespace MongoDbPoc.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController(IBooksService booksService) : ControllerBase
    {
        private readonly IBooksService _booksService = booksService;

        [HttpGet]
        public async Task<IActionResult> GetAsync(string id)
        {
            var result = await _booksService.GetAsync(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using MongoDbPoc.Extensions;
using MongoDbPoc.Models;
using MongoDbPoc.Models.Requests;
using MongoDbPoc.Services.Interfaces;

namespace MongoDbPoc.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController(IBooksService booksService) : ControllerBase
    {
        private readonly IBooksService _booksService = booksService;

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<Book?>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
        public async Task<IActionResult> GetAsync(string id)
        {
            var result = await _booksService.GetAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<Book>>))]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _booksService.GetAllAsync();
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse))]
        public async Task<IActionResult> CreateAsync(CreateBookRequest request)
        {
            var result = await _booksService.CreateAsync(request.AsModel());
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse))]
        public async Task<IActionResult> UpdateAsync(string id, UpdateBookRequest request)
        {
            var result = await _booksService.UpdateAsync(id, request.AsModel());
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse))]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var result = await _booksService.DeleteAsync(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}

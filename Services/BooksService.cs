using MongoDbPoc.DataGateway.Interfaces;
using MongoDbPoc.Exceptions;
using MongoDbPoc.Models;
using MongoDbPoc.Services.Interfaces;

namespace MongoDbPoc.Services
{
    public class BooksService(IBooksDataGateway booksDataGateway, ILogger<BooksService> logger) : IBooksService
    {
        private readonly IBooksDataGateway _booksDataGateway = booksDataGateway;
        private readonly ILogger<BooksService> _logger = logger;

        public async Task<ApiResponse> CreateAsync(Book newBook)
        {
            try
            {
                // TODO: perform validation

                await _booksDataGateway.CreateAsync(newBook);

                return new ApiResponse(true, StatusCodes.Status201Created, "Successfully created book.");
            }
            catch (Exception e)
            {
                var ex = new BookServiceException(e);
                _logger.LogError(ex, "Exception");
                throw;
            }
        }

        public async Task<ApiResponse> DeleteAsync(string id)
        {
            try
            {
                var book = await _booksDataGateway.GetAsync(id);

                if (book is null)
                {
                    return new ApiResponse(false, StatusCodes.Status404NotFound, "Book not found.");
                }

                await _booksDataGateway.RemoveAsync(id);

                return new ApiResponse(true, StatusCodes.Status204NoContent, "Successfully deleted book.");
            }
            catch (Exception e)
            {
                var ex = new BookServiceException(e);
                _logger.LogError(ex, "Exception");
                throw;
            }
        }

        public async Task<ApiResponse<IEnumerable<Book>>> GetAllAsync()
        {
            try
            {
                var books = await _booksDataGateway.GetAsync();

                return new ApiResponse<IEnumerable<Book>>(true, books, StatusCodes.Status200OK, "Successfuly retrieved all books.");
            }
            catch (Exception e)
            {
                var ex = new BookServiceException(e);
                _logger.LogError(ex, "Exception");
                throw;
            }
        }

        public async Task<ApiResponse<Book?>> GetAsync(string id)
        {
            try
            {
                var book = await _booksDataGateway.GetAsync(id);

                if (book is null)
                {
                    return new ApiResponse<Book?>(false, null, StatusCodes.Status404NotFound, "Book not found.");
                }

                return new ApiResponse<Book?>(true, book, StatusCodes.Status200OK, "Successfully retrived book.");
            }
            catch (Exception e)
            {
                var ex = new BookServiceException(e);
                _logger.LogError(ex, "Exception");
                throw;
            }
        }

        public async Task<ApiResponse> UpdateAsync(string id, Book updatedBook)
        {
            try
            {
                // TODO: perform validation

                var book = await _booksDataGateway.GetAsync(id);

                if (book is null)
                {
                    return new ApiResponse(false, StatusCodes.Status404NotFound, "Book not found.");
                }

                updatedBook.Id = book.Id;

                await _booksDataGateway.UpdateAsync(id, updatedBook);

                return new ApiResponse(true, StatusCodes.Status204NoContent, "Successfully updated book");
            }
            catch (Exception e)
            {
                var ex = new BookServiceException(e);
                _logger.LogError(ex, "Exception");
                throw;
            }
        }
    }
}

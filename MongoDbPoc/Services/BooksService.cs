using FluentValidation;
using MongoDbPoc.DataGateway.Interfaces;
using MongoDbPoc.Exceptions;
using MongoDbPoc.Models;
using MongoDbPoc.Services.Interfaces;

namespace MongoDbPoc.Services
{
    public class BooksService(IBooksDataGateway booksDataGateway, ILogger<BooksService> logger, IValidator<Book> validator) : IBooksService
    {
        private readonly IBooksDataGateway _booksDataGateway = booksDataGateway;
        private readonly ILogger<BooksService> _logger = logger;
        private readonly IValidator<Book> _validator = validator;

        public async Task<ApiResponse> CreateAsync(Book newBook)
        {
            var result = new ApiResponse();

            try
            {
                _validator.ValidateAndThrow(newBook);

                await _booksDataGateway.CreateAsync(newBook);

                result.StatusCode = StatusCodes.Status201Created;

                return result;
            }
            catch (ValidationException e)
            {
                result.StatusCode = StatusCodes.Status400BadRequest;
                result.Message = "books/validation-error";
                result.Errors = e.Errors.Select(e => e.ErrorMessage).ToList();

                return result;
            }
            catch (DataGatewayException)
            {
                throw;
            }
            catch (Exception e)
            {
                var ex = new BookServiceException(e);
                _logger.LogError(ex, "Exception");
                throw ex;
            }
        }

        public async Task<ApiResponse> DeleteAsync(string id)
        {
            var result = new ApiResponse();
            try
            {
                var book = await _booksDataGateway.GetAsync(id);

                if (book is null)
                {
                    result.StatusCode = StatusCodes.Status404NotFound;
                    result.Message = "books/not-found";
                    return result;
                }

                await _booksDataGateway.RemoveAsync(id);

                result.StatusCode = StatusCodes.Status204NoContent;

                return result;
            }
            catch (FormatException e)
            {
                result.StatusCode = StatusCodes.Status400BadRequest;
                result.Message = "books/invalid-id-format";
                result.Errors = [e.Message];

                return result;
            }
            catch (DataGatewayException)
            {
                throw;
            }
            catch (Exception e)
            {
                var ex = new BookServiceException(e);
                _logger.LogError(ex, "Exception");
                throw ex;
            }
        }

        public async Task<ApiResponse<IEnumerable<Book>>> GetAllAsync()
        {
            var result = new ApiResponse<IEnumerable<Book>>();

            try
            {
                var books = await _booksDataGateway.GetAsync();

                result.Data = books;
                result.StatusCode = StatusCodes.Status200OK;

                return result;
            }
            catch (DataGatewayException)
            {
                throw;
            }
            catch (Exception e)
            {
                var ex = new BookServiceException(e);
                _logger.LogError(ex, "Exception");
                throw ex;
            }
        }

        public async Task<ApiResponse<Book?>> GetAsync(string id)
        {
            var result = new ApiResponse<Book?>();

            try
            {
                var book = await _booksDataGateway.GetAsync(id);

                if (book is null)
                {
                    result.Data = null;
                    result.StatusCode = StatusCodes.Status404NotFound;
                    result.Message = "books/not-found";

                    return result;
                }

                result.Data = book;
                result.StatusCode = StatusCodes.Status200OK;

                return result;
            }
            catch (FormatException e)
            {
                result.StatusCode = StatusCodes.Status400BadRequest;
                result.Message = "books/invalid-id-format";
                result.Errors = [e.Message];

                return result;
            }
            catch (DataGatewayException)
            {
                throw;
            }
            catch (Exception e)
            {
                var ex = new BookServiceException(e);
                _logger.LogError(ex, "Exception");
                throw ex;
            }
        }

        public async Task<ApiResponse> UpdateAsync(string id, Book updatedBook)
        {
            var result = new ApiResponse();

            try
            {
                _validator.ValidateAndThrow(updatedBook);

                var book = await _booksDataGateway.GetAsync(id);

                if (book is null)
                {
                    result.StatusCode= StatusCodes.Status404NotFound;
                    result.Message = "books/not-found";

                    return result;
                }

                updatedBook.Id = book.Id;

                await _booksDataGateway.UpdateAsync(id, updatedBook);

                result.StatusCode = StatusCodes.Status204NoContent;

                return result;
            }
            catch (ValidationException e)
            {
                result.StatusCode = StatusCodes.Status400BadRequest;
                result.Message = "books/validation-error";
                result.Errors = e.Errors.Select(e => e.ErrorMessage).ToList();

                return result;
            }
            catch (FormatException e)
            {
                result.StatusCode = StatusCodes.Status400BadRequest;
                result.Message = "books/invalid-id-format";
                result.Errors = [e.Message];

                return result;
            }
            catch (DataGatewayException)
            {
                throw;
            }
            catch (Exception e)
            {
                var ex = new BookServiceException(e);
                _logger.LogError(ex, "Exception");
                throw ex;
            }
        }
    }
}

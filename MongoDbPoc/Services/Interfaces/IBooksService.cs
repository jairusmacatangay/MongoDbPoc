using MongoDbPoc.Models;

namespace MongoDbPoc.Services.Interfaces
{
    public interface IBooksService
    {
        public Task<ApiResponse<IEnumerable<Book>>> GetAllAsync();

        public Task<ApiResponse<Book?>> GetAsync(string id);

        public Task<ApiResponse> CreateAsync(Book newBook);

        public Task<ApiResponse> UpdateAsync(string id, Book updatedBook);

        public Task<ApiResponse> DeleteAsync(string id);
    }
}

using MongoDbPoc.Models;
using MongoDbPoc.Models.Requests;

namespace MongoDbPoc.Extensions
{
    public static class BooksExtensions
    {
        public static Book AsModel(this CreateBookRequest request)
        {
            return new Book
            {
                BookName = request.BookName ?? "",
                Price = request.Price,
                Category = request.Category ?? "",
                Author = request.Author ?? ""
            };
        }

        public static Book AsModel(this UpdateBookRequest request) 
        {
            return new Book
            {
                BookName = request.BookName ?? "",
                Price = request.Price,
                Category = request.Category ?? "",
                Author = request.Author ?? ""
            };
        }
    }
}

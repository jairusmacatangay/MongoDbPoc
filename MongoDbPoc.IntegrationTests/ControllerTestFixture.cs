
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbPoc.Models;

namespace MongoDbPoc.IntegrationTests
{
    public class ControllerTestFixture : IAsyncLifetime
    {
        public CustomWebApplicationFactory<Program> Factory { get; private set; }

        public ControllerTestFixture()
        {
            Factory = new CustomWebApplicationFactory<Program>();
        }

        public async Task InitializeAsync()
        {
            // Seed database
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("BookStoreIntegrationTest");
            var booksCollection = database.GetCollection<Book>("Books");

            await booksCollection.InsertOneAsync(new Book
            {
                Id = new ObjectId("66dbe61d7d478c9816c73bf8").ToString(),
                BookName = "Test Book",
                Price = 99.99M,
                Category = "Test",
                Author = "John Doe",
            });
        }

        public async Task DisposeAsync()
        {
            // Ensure the database is clean before tests
            var client = new MongoClient("mongodb://localhost:27017");
            await client.DropDatabaseAsync("BookStoreIntegrationTest");
        }
    }
}

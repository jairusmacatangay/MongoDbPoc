using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDbPoc.DataGateway.Interfaces;
using MongoDbPoc.Exceptions;
using MongoDbPoc.Models;

namespace MongoDbPoc.DataGateway
{
    public class BooksNoSqlDataGateway : IBooksDataGateway
    {
        private readonly IMongoCollection<Book> _booksCollection;
        private readonly ILogger<BooksNoSqlDataGateway> _logger;

        public BooksNoSqlDataGateway(
            IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings, 
            ILogger<BooksNoSqlDataGateway> logger)
        {
            var mongoClient = new MongoClient(bookStoreDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(bookStoreDatabaseSettings.Value.DatabaseName);
            _booksCollection = mongoDatabase.GetCollection<Book>(bookStoreDatabaseSettings.Value.BooksCollectionName);
            _logger = logger;
        }

        public async Task CreateAsync(Book newBook)
        {
            try
            {
                await _booksCollection.InsertOneAsync(newBook);
            }
            catch (Exception e)
            {
                var ex = new DataGatewayException(e);
                _logger.LogError(ex, "Exception");
                throw ex;
            }
        }

        public async Task<List<Book>> GetAsync()
        {
            try
            {
                return await _booksCollection.Find(_ => true).ToListAsync();
            }
            catch (Exception e)
            {
                var ex = new DataGatewayException(e);
                _logger.LogError(ex, "Exception");
                throw ex;
            }
        }

        public async Task<Book?> GetAsync(string id)
        {
            try
            {
                return await _booksCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            }
            catch (FormatException)
            {
                throw;
            }
            catch (Exception e)
            {
                var ex = new DataGatewayException(e);
                _logger.LogError(ex, "Exception");
                throw ex;
            }
        }

        public async Task RemoveAsync(string id)
        {
            try
            {
                await _booksCollection.DeleteOneAsync(x => x.Id == id);
            }
            catch (Exception e)
            {

                var ex = new DataGatewayException(e);
                _logger.LogError(ex, "Exception");
                throw ex;
            }
        }

        public async Task UpdateAsync(string id, Book updatedBook)
        {
            try
            {
                await _booksCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);
            }
            catch (Exception e)
            {
                var ex = new DataGatewayException(e);
                _logger.LogError(ex, "Exception");
                throw ex;
            }
        }
    }
}

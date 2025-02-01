using Microsoft.AspNetCore.Mvc.Testing;
using MongoDbPoc.Models;

namespace MongoDbPoc.IntegrationTests
{
    public class CustomWebApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> 
        where TEntryPoint : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(BookStoreDatabaseSettings));

                if (descriptor != null) 
                {
                    services.Remove(descriptor);
                }

                services.Configure<BookStoreDatabaseSettings>(options =>
                {
                    options.ConnectionString = "mongodb://localhost:27017";
                    options.DatabaseName = "BookStoreIntegrationTest";
                    options.BooksCollectionName = "Books";
                });
            });
        }
    }
}

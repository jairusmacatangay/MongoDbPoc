using MongoDbPoc.Models;
using MongoDbPoc.Models.Requests;
using System.Net;
using System.Text;
using System.Text.Json;

namespace MongoDbPoc.IntegrationTests
{
    public class BooksControllerTests(ControllerTestFixture fixture) : IClassFixture<ControllerTestFixture>
    {
        private readonly HttpClient _httpClient = fixture.Factory.CreateClient();
        private readonly string _url = "http://localhost:5678/api/books";

        [Fact]
        public async void CreateAsync_InsertBook_ReceivesStatus201()
        {
            // Arrange
            var json = JsonSerializer.Serialize(new CreateBookRequest
            {
                BookName = "CreateAsync Test",
                Price = 89.99M,
                Category = "Create",
                Author = "CreateAsync Test"
            });

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync(_url, content);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }
}
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MongoDbPoc.DataGateway.Interfaces;
using MongoDbPoc.Models;
using MongoDbPoc.Services;
using Moq;

namespace MongoDbPoc.UnitTests
{
    public class BooksServiceTest
    {
        private BooksService target;

        private Mock<IBooksDataGateway> dataGateway;
        private Mock<ILogger<BooksService>> logger;
        private Mock<IValidator<Book>> validator;

        public BooksServiceTest()
        {
            this.dataGateway = new Mock<IBooksDataGateway>();
            this.logger = new Mock<ILogger<BooksService>>();
            this.validator = new Mock<IValidator<Book>>();

            this.target = new BooksService(
                this.dataGateway.Object,
                this.logger.Object,
                this.validator.Object
            );
        }

        [Fact]
        public async void CreateAsync_Create_Returned204()
        {
            // Arrange
            var expected = new ApiResponse
            {
                StatusCode = StatusCodes.Status201Created
            };

            this.dataGateway.Setup(s => s.CreateAsync(It.IsAny<Book>()));

            // Act
            var actual = await this.target.CreateAsync(new Book());

            // Assert
            Assert.Equal(actual.StatusCode, expected.StatusCode);
        }
    }
}
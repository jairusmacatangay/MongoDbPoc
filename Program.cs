using FluentValidation;
using MongoDbPoc.DataGateway;
using MongoDbPoc.DataGateway.Interfaces;
using MongoDbPoc.Models;
using MongoDbPoc.Services;
using MongoDbPoc.Services.Interfaces;
using MongoDbPoc.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<BookStoreDatabaseSettings>(builder.Configuration.GetSection("BookStoreDatabase"));

// DI
builder.Services.AddSingleton<IBooksDataGateway, BooksNoSqlDataGateway>();
builder.Services.AddScoped<IBooksService, BooksService>();

// Validators
builder.Services.AddScoped<IValidator<Book>, BookValidator>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

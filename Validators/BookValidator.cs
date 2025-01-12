using FluentValidation;
using MongoDbPoc.Models;

namespace MongoDbPoc.Validators
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator() 
        {
            RuleFor(x => x.BookName).NotNull().NotEmpty();
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.Category).NotNull().NotEmpty();
            RuleFor(x => x.Author).NotNull().NotEmpty();
        }
    }
}

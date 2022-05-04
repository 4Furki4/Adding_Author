using FluentValidation;
using System;
namespace BookStore.Applications.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand> //İçine aldığı sınıfı tasdikler.
    {
        public CreateBookCommandValidator()
        {
            RuleFor(command=>command.Model.GenreId).GreaterThan(0);
            RuleFor(command=>command.Model.PageCount).GreaterThan(0);
            RuleFor(command=>command.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.Today);
            RuleFor(command=>command.Model.Title).NotEmpty().MinimumLength(4);
        }
    }
}
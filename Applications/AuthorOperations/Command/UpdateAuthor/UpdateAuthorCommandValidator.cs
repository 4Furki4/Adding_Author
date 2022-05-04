using FluentValidation;
using System;

namespace BookStore.Applications.AuthorOperations.Command.UpdateAuthor
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(x=>x.AuthorId).GreaterThan(0);
            RuleFor(x=>x.Model.Name).MinimumLength(2);
            RuleFor(x=>x.Model.Surname).MinimumLength(2);
            RuleFor(x=>x.Model.BirthDay).LessThan(new DateTime (2007,04,05)); //15 yaşından küçük yazar olamaz :D!
        }
    }
}
using FluentValidation;

namespace BookStore.Applications.AuthorOperations.Command.CreateAuthor
{
    public class CreateAuthorCommandValidation : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidation()
        {
            RuleFor(ex=> ex.AuthorId).GreaterThan(0);
            RuleFor(ex=> ex.Model.BirthDay).LessThan(System.DateTime.Now);
            RuleFor(ex=> ex.Model.Name).MinimumLength(2);
            RuleFor(ex=> ex.Model.Surname).MinimumLength(2);
        }
    }
}
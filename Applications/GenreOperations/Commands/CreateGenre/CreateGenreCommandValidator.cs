using FluentValidation;

namespace BookStore.Applications.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
    {
        public CreateGenreCommandValidator()
        {
            RuleFor(command=> command.Model.Name).NotEmpty().MinimumLength(2);
        }
    }
}
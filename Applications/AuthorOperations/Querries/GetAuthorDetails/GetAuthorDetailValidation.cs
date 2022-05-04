using FluentValidation;

namespace BookStore.Applications.AuthorOperations.Querries.GetAuthorDetails
{
    public class GetAuthorDetailValidation : AbstractValidator<GetAuthorDetail>
    {
        public GetAuthorDetailValidation()
        {
            RuleFor(querry=>querry.AuthorId).GreaterThan(0);
        }
    }
}
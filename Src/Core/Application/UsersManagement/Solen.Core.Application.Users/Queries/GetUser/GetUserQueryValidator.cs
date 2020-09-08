using FluentValidation;

namespace Solen.Core.Application.Users.Queries
{
    public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
    {
        public GetUserQueryValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
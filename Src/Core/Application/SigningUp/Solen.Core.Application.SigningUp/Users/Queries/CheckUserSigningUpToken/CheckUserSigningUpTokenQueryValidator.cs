using FluentValidation;

namespace Solen.Core.Application.SigningUp.Users.Queries
{
    public class CheckUserSigningUpTokenQueryValidator : AbstractValidator<CheckUserSigningUpTokenQuery>
    {
        public CheckUserSigningUpTokenQueryValidator()
        {
            RuleFor(x => x.SigningUpToken).NotEmpty();
        }
    }
}
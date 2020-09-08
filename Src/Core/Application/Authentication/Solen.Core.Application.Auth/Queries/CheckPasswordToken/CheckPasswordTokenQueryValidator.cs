using FluentValidation;

namespace Solen.Core.Application.Auth.Queries
{
    public class CheckPasswordTokenQueryValidator : AbstractValidator<CheckPasswordTokenQuery>
    {
        public CheckPasswordTokenQueryValidator()
        {
            RuleFor(x => x.PasswordToken).NotEmpty();
        }
    }
}
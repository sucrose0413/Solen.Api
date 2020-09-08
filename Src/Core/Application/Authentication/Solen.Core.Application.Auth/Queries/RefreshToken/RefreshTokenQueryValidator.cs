using FluentValidation;

namespace Solen.Core.Application.Auth.Queries
{
    public class RefreshTokenQueryValidator : AbstractValidator<RefreshTokenQuery>
    {
        public RefreshTokenQueryValidator()
        {
            RuleFor(x => x.RefreshToken).NotEmpty();
        }
    }
}
using FluentValidation;

namespace Solen.Core.Application.SigningUp.Organizations.Queries
{
    public class CheckOrganizationSigningUpTokenQueryValidator : AbstractValidator<CheckOrganizationSigningUpTokenQuery>
    {
        public CheckOrganizationSigningUpTokenQueryValidator()
        {
            RuleFor(x => x.SigningUpToken).NotEmpty();
        }
    }
}
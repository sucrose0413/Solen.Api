using FluentValidation;

namespace Solen.Core.Application.SigningUp.Organizations.Commands
{
    public class CompleteSigningUpCommandValidator : AbstractValidator<CompleteOrganizationSigningUpCommand>
    {
        public CompleteSigningUpCommandValidator()
        {
            RuleFor(x => x.SigningUpToken).NotEmpty();
            RuleFor(x => x.OrganizationName).NotEmpty().MaximumLength(60);
            RuleFor(x => x.UserName).NotEmpty().MaximumLength(30);
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.ConfirmPassword).NotEmpty();
            RuleFor(x => x.Password)
                .Equal(x => x.ConfirmPassword);
        }
    }
}
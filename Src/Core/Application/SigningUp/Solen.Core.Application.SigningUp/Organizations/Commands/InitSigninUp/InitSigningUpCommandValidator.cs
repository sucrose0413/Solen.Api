using FluentValidation;

namespace Solen.Core.Application.SigningUp.Organizations.Commands
{
    public class InitSigningUpCommandValidator : AbstractValidator<InitSigningUpCommand>
    {
        public InitSigningUpCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().MaximumLength(50).EmailAddress();
        }
    }
}
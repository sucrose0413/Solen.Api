using FluentValidation;

namespace Solen.Core.Application.SigningUp.Users.Commands
{
    public class CompleteSigningUpCommandValidator : AbstractValidator<CompleteUserSigningUpCommand>
    {
        public CompleteSigningUpCommandValidator()
        {
            RuleFor(x => x.SigningUpToken).NotEmpty();
            RuleFor(x => x.UserName).NotEmpty().MaximumLength(30);
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.ConfirmPassword).NotEmpty();
            RuleFor(x => x.Password)
                .Equal(x => x.ConfirmPassword);
        }
    }
}
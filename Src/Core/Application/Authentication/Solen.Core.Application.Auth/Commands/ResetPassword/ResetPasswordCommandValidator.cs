using FluentValidation;

namespace Solen.Core.Application.Auth.Commands
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(x => x.PasswordToken).NotEmpty();
            RuleFor(x => x.NewPassword).NotEmpty();
            RuleFor(x => x.ConfirmNewPassword).NotEmpty();
            RuleFor(x => x.NewPassword)
                .Equal(x => x.ConfirmNewPassword);
        }
    }
}
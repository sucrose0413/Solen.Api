using FluentValidation;

namespace Solen.Core.Application.UserProfile.Commands
{
    public class UpdateCurrentUserPasswordCommandValidator : AbstractValidator<UpdateCurrentUserPasswordCommand>
    {
        public UpdateCurrentUserPasswordCommandValidator()
        {
            RuleFor(x => x.ConfirmNewPassword).NotEmpty();
            RuleFor(x => x.NewPassword).NotEmpty()
                .Equal(x => x.ConfirmNewPassword);
        }
    }
}
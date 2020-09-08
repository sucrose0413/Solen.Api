using FluentValidation;

namespace Solen.Core.Application.UserProfile.Commands
{
    public class UpdateCurrentUserInfoCommandValidator : AbstractValidator<UpdateCurrentUserInfoCommand>
    {
        public UpdateCurrentUserInfoCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().MaximumLength(30);
        }
    }
}
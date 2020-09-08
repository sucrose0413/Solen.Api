using FluentValidation;

namespace Solen.Core.Application.Users.Commands
{
    public class UnblockUserCommandValidator : AbstractValidator<UnblockUserCommand>
    {
        public UnblockUserCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
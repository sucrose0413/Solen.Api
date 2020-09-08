using FluentValidation;

namespace Solen.Core.Application.Users.Commands
{
    public class BlockUserCommandValidator : AbstractValidator<BlockUserCommand>
    {
        public BlockUserCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
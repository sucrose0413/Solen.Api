using FluentValidation;

namespace Solen.Core.Application.Users.Commands
{
    public class UpdateUserRolesCommandValidator : AbstractValidator<UpdateUserRolesCommand>
    {
        public UpdateUserRolesCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.RolesIds).NotEmpty().WithMessage("At least one role is required");
        }
    }
}
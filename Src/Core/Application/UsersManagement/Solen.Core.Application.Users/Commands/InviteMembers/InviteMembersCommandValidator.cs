using FluentValidation;

namespace Solen.Core.Application.Users.Commands
{
    public class InviteMembersCommandValidator : AbstractValidator<InviteMembersCommand>
    {
        public InviteMembersCommandValidator()
        {
            RuleFor(x => x.LearningPathId).NotEmpty();
            RuleFor(x => x.Emails).NotEmpty();
            RuleForEach(x => x.Emails)
                .NotEmpty().EmailAddress().MaximumLength(50);
        }
    }
}

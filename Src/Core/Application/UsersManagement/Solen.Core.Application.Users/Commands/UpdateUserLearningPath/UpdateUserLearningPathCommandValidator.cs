using FluentValidation;

namespace Solen.Core.Application.Users.Commands
{
    public class UpdateUserLearningPathCommandValidator : AbstractValidator<UpdateUserLearningPathCommand>
    {
        public UpdateUserLearningPathCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.LearningPathId).NotEmpty();
        }
    }
}
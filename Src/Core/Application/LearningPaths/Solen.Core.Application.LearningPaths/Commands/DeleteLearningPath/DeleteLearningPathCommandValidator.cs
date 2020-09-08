using FluentValidation;

namespace Solen.Core.Application.LearningPaths.Commands
{
    public class DeleteLearningPathCommandValidator : AbstractValidator<DeleteLearningPathCommand>
    {
        public DeleteLearningPathCommandValidator()
        {
            RuleFor(x => x.LearningPathId).NotEmpty();
        }
    }
}
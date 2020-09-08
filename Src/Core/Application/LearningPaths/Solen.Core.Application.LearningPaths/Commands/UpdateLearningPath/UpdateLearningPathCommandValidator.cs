using FluentValidation;

namespace Solen.Core.Application.LearningPaths.Commands
{
    public class UpdateLearningPathCommandValidator : AbstractValidator<UpdateLearningPathCommand>
    {
        public UpdateLearningPathCommandValidator()
        {
            RuleFor(x => x.LearningPathId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Description).MaximumLength(100);
        }
    }
}
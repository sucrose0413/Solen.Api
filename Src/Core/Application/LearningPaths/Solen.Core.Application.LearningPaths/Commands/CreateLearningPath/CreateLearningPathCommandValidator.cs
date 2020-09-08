using FluentValidation;

namespace Solen.Core.Application.LearningPaths.Commands
{
    public class CreateLearningPathCommandValidator : AbstractValidator<CreateLearningPathCommand>
    {
        public CreateLearningPathCommandValidator()
        {
            RuleFor(x => x.Name).MaximumLength(50).NotEmpty();
        }
    }
}
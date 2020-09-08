using FluentValidation;

namespace Solen.Core.Application.LearningPaths.Commands
{
    public class AddCoursesToLearningPathCommandValidator : AbstractValidator<AddCoursesToLearningPathCommand>
    {
        public AddCoursesToLearningPathCommandValidator()
        {
            RuleFor(x => x.LearningPathId).NotEmpty();
            RuleFor(x => x.CoursesIds).NotNull();
        }
    }
}
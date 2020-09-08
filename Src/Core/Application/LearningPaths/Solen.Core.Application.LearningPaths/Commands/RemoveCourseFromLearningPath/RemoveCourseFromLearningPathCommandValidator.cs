using FluentValidation;

namespace Solen.Core.Application.LearningPaths.Commands
{
    public class RemoveCourseFromLearningPathCommandValidator : AbstractValidator<RemoveCourseFromLearningPathCommand>
    {
        public RemoveCourseFromLearningPathCommandValidator()
        {
            RuleFor(x => x.CourseId).NotEmpty();
            RuleFor(x => x.LearningPathId).NotEmpty();
        }
    }
}
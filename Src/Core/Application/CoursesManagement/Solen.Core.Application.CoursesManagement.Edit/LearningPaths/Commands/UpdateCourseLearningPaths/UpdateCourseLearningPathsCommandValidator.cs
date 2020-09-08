using FluentValidation;

namespace Solen.Core.Application.CoursesManagement.Edit.LearningPaths.Commands
{
    public class UpdateCourseLearningPathsCommandValidator : AbstractValidator<UpdateCourseLearningPathsCommand>
    {
        public UpdateCourseLearningPathsCommandValidator()
        {
            RuleFor(x => x.CourseId).NotEmpty();
            RuleFor(x => x.LearningPathsIds).NotEmpty();
        }
    }
}
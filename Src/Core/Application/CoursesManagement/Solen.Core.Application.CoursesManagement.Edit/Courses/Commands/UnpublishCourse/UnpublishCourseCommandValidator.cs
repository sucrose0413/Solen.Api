using FluentValidation;


namespace Solen.Core.Application.CoursesManagement.Edit.Courses.Commands
{
    public class UnpublishCourseCommandValidator : AbstractValidator<UnpublishCourseCommand>
    {
        public UnpublishCourseCommandValidator()
        {
            RuleFor(x => x.CourseId).NotEmpty();
        }
    }
}
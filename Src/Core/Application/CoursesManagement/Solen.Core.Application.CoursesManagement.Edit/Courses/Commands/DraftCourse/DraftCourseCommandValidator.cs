using FluentValidation;


namespace Solen.Core.Application.CoursesManagement.Edit.Courses.Commands
{
    public class DraftCourseCommandValidator : AbstractValidator<DraftCourseCommand>
    {
        public DraftCourseCommandValidator()
        {
            RuleFor(x => x.CourseId).NotEmpty();
        }
    }
}
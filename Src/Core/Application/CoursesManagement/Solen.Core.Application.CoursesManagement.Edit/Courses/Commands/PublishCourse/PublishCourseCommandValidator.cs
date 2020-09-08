using FluentValidation;

namespace Solen.Core.Application.CoursesManagement.Edit.Courses.Commands
{
    public class PublishCourseCommandValidator : AbstractValidator<PublishCourseCommand>
    {
        public PublishCourseCommandValidator()
        {
            RuleFor(x => x.CourseId).NotEmpty();
        }
    }
}
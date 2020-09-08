using FluentValidation;


namespace Solen.Core.Application.CoursesManagement.Edit.Courses.Commands
{
    public class DeleteCourseCommandValidator : AbstractValidator<DeleteCourseCommand>
    {
        public DeleteCourseCommandValidator()
        {
            RuleFor(x => x.CourseId).NotEmpty();
        }
    }
}
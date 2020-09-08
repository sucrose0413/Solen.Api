using FluentValidation;

namespace Solen.Core.Application.CoursesManagement.Edit.Courses.Commands
{
    public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
    {
        public CreateCourseCommandValidator()
        {
            RuleFor(x => x.Title).MaximumLength(60).NotEmpty();
        }
    }
}

using FluentValidation;

namespace Solen.Core.Application.CoursesManagement.Edit.Lectures.Commands
{
    public class CreateLectureCommandValidator : AbstractValidator<CreateLectureCommand>
    {
        public CreateLectureCommandValidator()
        {
            RuleFor(x => x.Title).MaximumLength(100).NotEmpty();
            RuleFor(x => x.ModuleId).NotEmpty();
            RuleFor(x => x.LectureType).NotEmpty();
        }
    }
}
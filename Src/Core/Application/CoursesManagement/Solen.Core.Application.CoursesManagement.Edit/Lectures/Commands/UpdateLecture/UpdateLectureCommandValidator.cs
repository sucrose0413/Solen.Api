using FluentValidation;


namespace Solen.Core.Application.CoursesManagement.Edit.Lectures.Commands
{
    public class UpdateLectureCommandValidator : AbstractValidator<UpdateLectureCommand>
    {
        public UpdateLectureCommandValidator()
        {
            RuleFor(x => x.LectureId).NotEmpty();
            RuleFor(x => x.Title).MaximumLength(100).NotEmpty();
        }
    }
}
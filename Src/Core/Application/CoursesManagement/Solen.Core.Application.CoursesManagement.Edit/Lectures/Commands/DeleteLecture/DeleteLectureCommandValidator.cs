using FluentValidation;


namespace Solen.Core.Application.CoursesManagement.Edit.Lectures.Commands
{
    public class DeleteLectureCommandValidator : AbstractValidator<DeleteLectureCommand>
    {
        public DeleteLectureCommandValidator()
        {
            RuleFor(x => x.LectureId).NotEmpty();
        }
    }
}
using FluentValidation;


namespace Solen.Core.Application.CoursesManagement.Edit.Lectures.Commands
{
    public class UploadMediaCommandValidator : AbstractValidator<UploadMediaCommand>
    {
        public UploadMediaCommandValidator()
        {
            RuleFor(x => x.LectureId).NotEmpty();
            RuleFor(x => x.LectureType).NotEmpty();
        }
    }
}
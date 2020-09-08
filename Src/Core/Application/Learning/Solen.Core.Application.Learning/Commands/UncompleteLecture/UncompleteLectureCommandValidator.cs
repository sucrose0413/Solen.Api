using FluentValidation;

namespace Solen.Core.Application.Learning.Commands
{
    public class UncompleteLectureCommandValidator : AbstractValidator<UncompleteLectureCommand>
    {
        public UncompleteLectureCommandValidator()
        {
            RuleFor(x => x.LectureId).NotEmpty();
        }
    }
}
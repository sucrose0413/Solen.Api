using FluentValidation;

namespace Solen.Core.Application.Learning.Commands
{
    public class CompleteLectureCommandValidator : AbstractValidator<CompleteLectureCommand>
    {
        public CompleteLectureCommandValidator()
        {
            RuleFor(x => x.LectureId).NotEmpty();
        }
    }
}
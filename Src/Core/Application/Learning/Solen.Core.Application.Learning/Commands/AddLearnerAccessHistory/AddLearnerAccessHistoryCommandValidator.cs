using FluentValidation;

namespace Solen.Core.Application.Learning.Commands
{
    public class AddLearnerAccessHistoryCommandValidator : AbstractValidator<AddLearnerAccessHistoryCommand>
    {
        public AddLearnerAccessHistoryCommandValidator()
        {
            RuleFor(x => x.LectureId).NotEmpty();
        }
    }
}
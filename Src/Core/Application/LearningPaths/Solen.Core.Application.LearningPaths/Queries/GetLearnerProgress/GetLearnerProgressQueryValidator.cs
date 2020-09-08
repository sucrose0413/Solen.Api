using FluentValidation;

namespace Solen.Core.Application.LearningPaths.Queries
{
    public class GetLearnerProgressQueryValidator : AbstractValidator<GetLearnerProgressQuery>
    {
        public GetLearnerProgressQueryValidator()
        {
            RuleFor(x => x.LearnerId).NotEmpty();
        }
    }
}
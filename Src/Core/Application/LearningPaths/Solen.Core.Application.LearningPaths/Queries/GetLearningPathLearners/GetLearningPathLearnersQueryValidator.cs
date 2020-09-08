using FluentValidation;

namespace Solen.Core.Application.LearningPaths.Queries
{
    public class GetLearningPathLearnersQueryValidator : AbstractValidator<GetLearningPathLearnersQuery>
    {
        public GetLearningPathLearnersQueryValidator()
        {
            RuleFor(x => x.LearningPathId).NotEmpty();
        }
    }
}
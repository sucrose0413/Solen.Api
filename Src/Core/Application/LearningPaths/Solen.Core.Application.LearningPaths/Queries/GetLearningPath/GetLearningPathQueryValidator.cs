using FluentValidation;

namespace Solen.Core.Application.LearningPaths.Queries
{
    public class GetLearningPathQueryValidator : AbstractValidator<GetLearningPathQuery>
    {
        public GetLearningPathQueryValidator()
        {
            RuleFor(x => x.LearningPathId).NotEmpty();
        }
    }
}
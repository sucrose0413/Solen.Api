using FluentValidation;

namespace Solen.Core.Application.LearningPaths.Queries
{
    public class GetOtherCoursesToAddQueryValidator : AbstractValidator<GetOtherCoursesToAddQuery>
    {
        public GetOtherCoursesToAddQueryValidator()
        {
            RuleFor(x => x.LearningPathId).NotEmpty();
        }
    }
}
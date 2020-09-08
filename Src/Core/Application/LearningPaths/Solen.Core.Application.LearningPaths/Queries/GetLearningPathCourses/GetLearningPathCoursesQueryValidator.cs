using FluentValidation;

namespace Solen.Core.Application.LearningPaths.Queries
{
    public class GetLearningPathCoursesQueryValidator : AbstractValidator<GetLearningPathCoursesQuery>
    {
        public GetLearningPathCoursesQueryValidator()
        {
            RuleFor(x => x.LearningPathId).NotEmpty();
        }
    }
}
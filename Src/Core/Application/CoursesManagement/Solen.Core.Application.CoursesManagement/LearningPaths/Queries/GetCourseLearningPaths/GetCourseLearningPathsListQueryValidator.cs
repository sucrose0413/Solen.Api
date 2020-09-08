using FluentValidation;

namespace Solen.Core.Application.CoursesManagement.LearningPaths.Queries
{
    public class GetCourseLearningPathsListQueryValidator : AbstractValidator<GetCourseLearningPathsListQuery>
    {
        public GetCourseLearningPathsListQueryValidator()
        {
            RuleFor(x => x.CourseId).NotEmpty();
        }
    }
}
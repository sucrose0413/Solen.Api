using FluentValidation;

namespace Solen.Core.Application.Learning.Queries
{
    public class GetCourseOverviewQueryValidator : AbstractValidator<GetCourseOverviewQuery>
    {
        public GetCourseOverviewQueryValidator()
        {
            RuleFor(x => x.CourseId).NotEmpty();
        }
    }
}
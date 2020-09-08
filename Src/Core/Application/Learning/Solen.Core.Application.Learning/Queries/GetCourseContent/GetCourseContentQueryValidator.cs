using FluentValidation;

namespace Solen.Core.Application.Learning.Queries
{
    public class GetCourseContentQueryValidator : AbstractValidator<GetCourseContentQuery>
    {
        public GetCourseContentQueryValidator()
        {
            RuleFor(x => x.CourseId).NotEmpty();
        }
    }
}
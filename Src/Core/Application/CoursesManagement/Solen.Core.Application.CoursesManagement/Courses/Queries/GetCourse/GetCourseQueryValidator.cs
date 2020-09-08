using FluentValidation;

namespace Solen.Core.Application.CoursesManagement.Courses.Queries
{
    public class GetCourseQueryValidator : AbstractValidator<GetCourseQuery>
    {
        public GetCourseQueryValidator()
        {
            RuleFor(x => x.CourseId).NotEmpty();
        }
    }
}
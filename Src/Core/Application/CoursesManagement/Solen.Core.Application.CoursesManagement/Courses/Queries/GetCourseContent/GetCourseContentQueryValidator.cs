using FluentValidation;

namespace Solen.Core.Application.CoursesManagement.Courses.Queries
{
    public class GetCourseContentQueryValidator : AbstractValidator<GetCourseContentQuery>
    {
        public GetCourseContentQueryValidator()
        {
            RuleFor(x => x.CourseId).NotEmpty();
        }
    }
}
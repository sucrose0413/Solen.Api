using MediatR;

namespace Solen.Core.Application.CoursesManagement.Courses.Queries
{
    public class GetCourseContentQuery : IRequest<CourseContentViewModel>
    {
        public string CourseId { get; set; }
    }
}

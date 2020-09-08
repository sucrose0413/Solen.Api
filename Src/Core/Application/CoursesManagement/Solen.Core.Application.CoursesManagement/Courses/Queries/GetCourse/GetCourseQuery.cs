using MediatR;


namespace Solen.Core.Application.CoursesManagement.Courses.Queries
{
    public class GetCourseQuery : IRequest<CourseViewModel>
    {
        public string CourseId { get; set; }
    }
}

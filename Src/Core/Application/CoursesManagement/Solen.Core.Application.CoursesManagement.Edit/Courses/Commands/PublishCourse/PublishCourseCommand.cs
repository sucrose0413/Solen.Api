using MediatR;

namespace Solen.Core.Application.CoursesManagement.Edit.Courses.Commands
{
    public class PublishCourseCommand : IRequest
    {
        public string CourseId { get; set; }
    }
}

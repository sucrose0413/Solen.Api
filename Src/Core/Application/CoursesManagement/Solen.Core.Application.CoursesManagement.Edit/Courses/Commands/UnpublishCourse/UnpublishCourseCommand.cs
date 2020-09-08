using MediatR;

namespace Solen.Core.Application.CoursesManagement.Edit.Courses.Commands
{
    public class UnpublishCourseCommand : IRequest
    {
        public string CourseId { get; set; }
    }
}
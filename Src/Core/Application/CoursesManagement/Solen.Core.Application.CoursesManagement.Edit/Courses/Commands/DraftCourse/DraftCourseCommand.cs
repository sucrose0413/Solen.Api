using MediatR;

namespace Solen.Core.Application.CoursesManagement.Edit.Courses.Commands
{
    public class DraftCourseCommand : IRequest
    {
        public string CourseId { get; set; }
    }
}
using MediatR;

namespace Solen.Core.Application.CoursesManagement.Edit.Courses.Commands
{
    public class DeleteCourseCommand : IRequest<CommandResponse>
    {
        public string CourseId { get; set; }
    }
}

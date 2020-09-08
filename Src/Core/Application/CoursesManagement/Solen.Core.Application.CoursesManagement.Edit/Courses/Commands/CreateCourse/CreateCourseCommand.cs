using MediatR;

namespace Solen.Core.Application.CoursesManagement.Edit.Courses.Commands
{
    public class CreateCourseCommand : IRequest<CommandResponse>
    {
        public string Title { get; set; }
    }
}

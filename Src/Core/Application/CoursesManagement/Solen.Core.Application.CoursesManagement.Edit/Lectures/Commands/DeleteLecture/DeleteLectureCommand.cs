using MediatR;

namespace Solen.Core.Application.CoursesManagement.Edit.Lectures.Commands
{
    public class DeleteLectureCommand : IRequest<CommandResponse>
    {
        public string LectureId { get; set; }
    }
}

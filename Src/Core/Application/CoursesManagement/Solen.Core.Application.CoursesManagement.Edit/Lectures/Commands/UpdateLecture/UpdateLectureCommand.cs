using MediatR;

namespace Solen.Core.Application.CoursesManagement.Edit.Lectures.Commands
{
    public class UpdateLectureCommand : IRequest
    {
        public string LectureId { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public string Content { get; set; }
    }
}

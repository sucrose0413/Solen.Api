using MediatR;


namespace Solen.Core.Application.CoursesManagement.Edit.Lectures.Commands
{
    public class CreateLectureCommand : IRequest<CommandResponse>
    {
        public string Title { get; set; }
        public string LectureType { get; set; }
        public int Order { get; set; }
        public int Duration { get; set; }
        public string Content { get; set; }
        public string ModuleId { get; set; }
    }
}
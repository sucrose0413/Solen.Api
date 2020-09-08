using MediatR;

namespace Solen.Core.Application.CoursesManagement.Edit.Lectures.Commands
{
    public class LectureDeletedEvent : INotification
    {
        public LectureDeletedEvent(string lectureId)
        {
            LectureId = lectureId;
        }
        public string LectureId { get;}
    }
}

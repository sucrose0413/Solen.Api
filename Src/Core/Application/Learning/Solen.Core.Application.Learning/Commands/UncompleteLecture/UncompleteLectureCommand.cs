using MediatR;

namespace Solen.Core.Application.Learning.Commands
{
    public class UncompleteLectureCommand : IRequest
    {
        public UncompleteLectureCommand(string lectureId)
        {
            LectureId = lectureId;
        }

        public string LectureId { get; }
    }
}
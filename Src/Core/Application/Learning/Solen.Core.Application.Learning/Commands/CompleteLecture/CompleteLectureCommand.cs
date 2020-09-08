using MediatR;

namespace Solen.Core.Application.Learning.Commands
{
    public class CompleteLectureCommand : IRequest
    {
        public string LectureId { get; set; }
    }
}
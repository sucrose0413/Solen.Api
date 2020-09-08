using MediatR;

namespace Solen.Core.Application.Learning.Commands
{
    public class AddLearnerAccessHistoryCommand : IRequest
    {
        public string LectureId { get; set; }
    }
}
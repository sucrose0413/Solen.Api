using MediatR;

namespace Solen.Core.Application.LearningPaths.Commands
{
    public class DeleteLearningPathCommand : IRequest<CommandResponse>
    {
        public string LearningPathId { get; set; }
    }
}
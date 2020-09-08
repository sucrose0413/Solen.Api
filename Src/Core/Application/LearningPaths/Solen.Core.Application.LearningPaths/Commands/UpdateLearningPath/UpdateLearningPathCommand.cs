using MediatR;

namespace Solen.Core.Application.LearningPaths.Commands
{
    public class UpdateLearningPathCommand : IRequest
    {
        public string LearningPathId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
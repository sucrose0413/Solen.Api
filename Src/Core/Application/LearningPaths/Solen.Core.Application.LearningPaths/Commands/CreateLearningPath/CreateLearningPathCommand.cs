using MediatR;

namespace Solen.Core.Application.LearningPaths.Commands
{
    public class CreateLearningPathCommand : IRequest<CommandResponse>
    {
        public string Name { get; set; }
    }
}

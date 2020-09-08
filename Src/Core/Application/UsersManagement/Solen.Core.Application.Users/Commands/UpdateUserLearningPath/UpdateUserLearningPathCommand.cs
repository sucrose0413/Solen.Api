using MediatR;

namespace Solen.Core.Application.Users.Commands
{
    public class UpdateUserLearningPathCommand : IRequest
    {
        public string UserId { get; set; }
        public string LearningPathId { get; set; }
    }
}
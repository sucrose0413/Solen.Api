using MediatR;

namespace Solen.Core.Application.LearningPaths.Commands
{
    public class RemoveCourseFromLearningPathCommand : IRequest
    {
        public string LearningPathId { get; set; }
        public string CourseId { get; set; }
    }
}
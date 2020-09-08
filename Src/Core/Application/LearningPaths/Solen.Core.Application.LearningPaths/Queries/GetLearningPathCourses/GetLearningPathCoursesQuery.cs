using MediatR;

namespace Solen.Core.Application.LearningPaths.Queries
{
    public class GetLearningPathCoursesQuery : IRequest<LearningPathCoursesViewModel>
    {
        public GetLearningPathCoursesQuery(string learningPathId)
        {
            LearningPathId = learningPathId;
        }

        public string LearningPathId { get; }
    }
}
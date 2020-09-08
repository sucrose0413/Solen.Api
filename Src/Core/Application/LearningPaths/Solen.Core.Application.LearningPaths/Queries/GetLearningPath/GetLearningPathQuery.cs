using MediatR;

namespace Solen.Core.Application.LearningPaths.Queries
{
    public class GetLearningPathQuery : IRequest<LearningPathViewModel>
    {
        public GetLearningPathQuery(string learningPathId)
        {
            LearningPathId = learningPathId;
        }

        public string LearningPathId { get; }
    }
}
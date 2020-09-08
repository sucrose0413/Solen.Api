using MediatR;

namespace Solen.Core.Application.LearningPaths.Queries
{
    public class GetLearningPathLearnersQuery : IRequest<LearningPathLearnersViewModel>
    {
        public GetLearningPathLearnersQuery(string learningPathId)
        {
            LearningPathId = learningPathId;
        }

        public string LearningPathId { get;}
    }
}
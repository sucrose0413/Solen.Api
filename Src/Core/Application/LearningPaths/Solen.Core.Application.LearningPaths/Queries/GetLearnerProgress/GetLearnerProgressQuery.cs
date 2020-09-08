using MediatR;

namespace Solen.Core.Application.LearningPaths.Queries
{
    public class GetLearnerProgressQuery : IRequest<LearnerProgressViewModel>
    {
        public GetLearnerProgressQuery(string learnerId)
        {
            LearnerId = learnerId;
        }
        public string LearnerId { get; }
    }
}
using MediatR;

namespace Solen.Core.Application.LearningPaths.Queries
{
    public class GetOtherCoursesToAddQuery : IRequest<OtherCoursesToAddViewModel>
    {
        public GetOtherCoursesToAddQuery(string learningPathId)
        {
            LearningPathId = learningPathId;
        }

        public string LearningPathId { get; }
    }
}
using MediatR;

namespace Solen.Core.Application.LearningPaths.Queries
{
    public class GetLearningPathsQuery : IRequest<LearningPathsViewModel>
    {
    }
}
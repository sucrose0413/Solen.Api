using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.LearningPaths.Queries;

namespace Solen.Core.Application.LearningPaths.Services.Queries
{
    public interface IGetLearningPathRepository
    {
        Task<LearningPathDto> GetLearningPath(string learningPathId, string organizationId, CancellationToken token);
    }
}
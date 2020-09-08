using System.Threading;
using System.Threading.Tasks;

namespace Solen.Core.Application.LearningPaths.Queries
{
    public interface IGetLearningPathService
    {
        Task<LearningPathDto> GetLearningPath(string learningPathId, CancellationToken token);
    }
}
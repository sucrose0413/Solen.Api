using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Solen.Core.Application.LearningPaths.Queries
{
    public interface IGetLearningPathsService
    {
        Task<IList<LearningPathDto>> GetLearningPaths(CancellationToken token);
    }
}
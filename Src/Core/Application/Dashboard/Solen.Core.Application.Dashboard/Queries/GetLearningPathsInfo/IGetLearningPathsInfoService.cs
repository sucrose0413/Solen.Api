using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Solen.Core.Application.Dashboard.Queries
{
    public interface IGetLearningPathsInfoService
    {
        Task<List<LearningPathForDashboardDto>> GetLearningPaths(CancellationToken token);
    }
}
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Dashboard.Queries;

namespace Solen.Core.Application.Dashboard.Services.Queries
{
    public interface IGetLearningPathsInfoRepository
    {
        Task<List<LearningPathForDashboardDto>> GetLearningPaths(string organizationId, CancellationToken token);
    }
}
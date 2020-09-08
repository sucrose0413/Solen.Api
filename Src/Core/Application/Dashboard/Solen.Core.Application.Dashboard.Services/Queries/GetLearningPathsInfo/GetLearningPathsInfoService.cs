using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Dashboard.Queries;

namespace Solen.Core.Application.Dashboard.Services.Queries
{
    public class GetLearningPathsInfoService : IGetLearningPathsInfoService
    {
        private readonly IGetLearningPathsInfoRepository _repo;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public GetLearningPathsInfoService(IGetLearningPathsInfoRepository repo,
            ICurrentUserAccessor currentUserAccessor)
        {
            _repo = repo;
            _currentUserAccessor = currentUserAccessor;
        }

        public async Task<List<LearningPathForDashboardDto>> GetLearningPaths(CancellationToken token)
        {
            return await _repo.GetLearningPaths(_currentUserAccessor.OrganizationId, token);
        }
    }
}
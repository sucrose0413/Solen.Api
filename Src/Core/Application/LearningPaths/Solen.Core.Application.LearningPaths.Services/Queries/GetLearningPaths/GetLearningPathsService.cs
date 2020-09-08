using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.LearningPaths.Queries;

namespace Solen.Core.Application.LearningPaths.Services.Queries
{
    public class GetLearningPathsService : IGetLearningPathsService
    {
        private readonly IGetLearningPathsRepository _repo;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public GetLearningPathsService(IGetLearningPathsRepository repo, ICurrentUserAccessor currentUserAccessor)
        {
            _repo = repo;
            _currentUserAccessor = currentUserAccessor;
        }

        public async Task<IList<LearningPathDto>> GetLearningPaths(CancellationToken token)
        {
            return await _repo.GetLearningPaths(_currentUserAccessor.OrganizationId, token);
        }
    }
}
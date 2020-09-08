using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.LearningPaths.Queries;

namespace Solen.Core.Application.LearningPaths.Services.Queries
{
    public class GetLearningPathService : IGetLearningPathService
    {
        private readonly IGetLearningPathRepository _repo;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public GetLearningPathService(IGetLearningPathRepository repo, ICurrentUserAccessor currentUserAccessor)
        {
            _repo = repo;
            _currentUserAccessor = currentUserAccessor;
        }

        public async Task<LearningPathDto> GetLearningPath(string learningPathId, CancellationToken token)
        {
            return await _repo.GetLearningPath(learningPathId, _currentUserAccessor.OrganizationId, token) ??
                   throw new NotFoundException($"The Learning Path ({learningPathId}) does not exist");
        }
    }
}
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.LearningPaths.Queries;

namespace Solen.Core.Application.LearningPaths.Services.Queries
{
    public class GetOtherCoursesToAddService : IGetOtherCoursesToAddService
    {
        private readonly IGetOtherCoursesToAddRepository _repo;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public GetOtherCoursesToAddService(IGetOtherCoursesToAddRepository repo,
            ICurrentUserAccessor currentUserAccessor)
        {
            _repo = repo;
            _currentUserAccessor = currentUserAccessor;
        }

        public async Task<IList<CourseForLearningPathDto>> GetOtherCoursesToAdd(string learningPathId,
            CancellationToken token)
        {
            return await _repo.GetCoursesToAdd(learningPathId, _currentUserAccessor.OrganizationId, token);
        }
    }
}
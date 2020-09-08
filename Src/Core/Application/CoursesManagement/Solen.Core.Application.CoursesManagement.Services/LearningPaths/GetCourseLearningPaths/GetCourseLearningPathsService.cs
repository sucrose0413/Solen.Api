using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.CoursesManagement.LearningPaths.Queries;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.CoursesManagement.Common;

namespace Solen.Core.Application.CoursesManagement.Services.LearningPaths
{
    public class GetCourseLearningPathsService : IGetCourseLearningPathsService
    {
        private readonly IGetCourseLearningPathsRepository _repo;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public GetCourseLearningPathsService(IGetCourseLearningPathsRepository repo,
            ICurrentUserAccessor currentUserAccessor)
        {
            _repo = repo;
            _currentUserAccessor = currentUserAccessor;
        }

        public async Task<IList<string>> GetCourseLearningPathsIds(string courseId, CancellationToken token)
        {
            return await _repo.GetCourseLearningPathsIds(courseId, _currentUserAccessor.OrganizationId, token);
        }

        public async Task<IList<CourseLearningPathDto>> GetOrganizationLearningPaths(CancellationToken token)
        {
            return await _repo.GetOrganizationLearningPaths(_currentUserAccessor.OrganizationId, token);
        }
    }
}
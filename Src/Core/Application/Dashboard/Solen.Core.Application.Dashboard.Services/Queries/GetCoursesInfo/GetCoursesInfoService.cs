using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Dashboard.Queries;
using Solen.Core.Domain.Courses.Enums.CourseStatuses;

namespace Solen.Core.Application.Dashboard.Services.Queries
{
    public class GetCoursesInfoService : IGetCoursesInfoService
    {
        private readonly IGetCoursesInfoRepository _repo;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public GetCoursesInfoService(IGetCoursesInfoRepository repo, ICurrentUserAccessor currentUserAccessor)
        {
            _repo = repo;
            _currentUserAccessor = currentUserAccessor;
        }

        public async Task<LastCreatedCourseDto> GetLastCreatedCourse(CancellationToken token)
        {
            return await _repo.GetLastCreatedCourse(_currentUserAccessor.OrganizationId, token);
        }

        public async Task<LastPublishedCourseDto> GetLastPublishedCourse(CancellationToken token)
        {
            return await _repo.GetLastPublishedCourse(_currentUserAccessor.OrganizationId,  new PublishedStatus().Name, token);
        }

        public async Task<int> GetCourseCount(CancellationToken token)
        {
            return await _repo.GetCourseCount(_currentUserAccessor.OrganizationId, token);
        }
    }
}
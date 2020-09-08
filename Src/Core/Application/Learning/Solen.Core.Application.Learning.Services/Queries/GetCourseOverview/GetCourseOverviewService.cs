using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Learning.Queries;
using Solen.Core.Domain.Courses.Enums.CourseStatuses;

namespace Solen.Core.Application.Learning.Services.Queries
{
    public class GetCourseOverviewService : IGetCourseOverviewService
    {
        private readonly IGetCourseOverviewRepository _repo;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public GetCourseOverviewService(IGetCourseOverviewRepository repo, ICurrentUserAccessor currentUserAccessor)
        {
            _repo = repo;
            _currentUserAccessor = currentUserAccessor;
        }

        public async Task<LearnerCourseOverviewDto> GetCourseOverview(string courseId, CancellationToken token)
        {
            return await _repo.GetCourseOverview(courseId, _currentUserAccessor.LearningPathId, new PublishedStatus().Name, token) ??
                   throw new NotFoundException($"The course ({courseId}) does not exist");
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Dashboard.Queries;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.Dashboard.Services.Queries
{
    public class GetLearnerInfoService : IGetLearnerInfoService
    {
        private readonly IGetLearnerInfoRepository _repo;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public GetLearnerInfoService(IGetLearnerInfoRepository repo, ICurrentUserAccessor currentUserAccessor)
        {
            _repo = repo;
            _currentUserAccessor = currentUserAccessor;
        }

        public async Task<Course> GetLastCourse(CancellationToken token)
        {
            return await _repo.GetLastAccessedCourse(_currentUserAccessor.UserId, token) ??
                   await _repo.GetLearningPathFirstCourse(_currentUserAccessor.LearningPathId, token);
        }

        public async Task<LearnerLastCourseProgressDto> GetLastCourseProgress(Course lastCourse,
            CancellationToken token)
        {
            if (lastCourse == null)
                return null;

            var totalDuration = await _repo.GetCourseTotalDuration(lastCourse.Id, token);
            var completedDuration =
                await _repo.GetLearnerCompletedDuration(_currentUserAccessor.UserId, lastCourse.Id, token);

            return new LearnerLastCourseProgressDto
            {
                CourseId = lastCourse.Id,
                CourseTitle = lastCourse.Title,
                CompletedDuration = completedDuration,
                TotalDuration = totalDuration
            };
        }
    }
}
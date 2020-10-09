using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Learning.Queries;
using Solen.Core.Domain.Courses.Enums.CourseStatuses;

namespace Solen.Core.Application.Learning.Services.Queries
{
    public class GetCourseContentService : IGetCourseContentService
    {
        private readonly IGetCourseContentRepository _repo;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public GetCourseContentService(IGetCourseContentRepository repo, ICurrentUserAccessor currentUserAccessor)
        {
            _repo = repo;
            _currentUserAccessor = currentUserAccessor;
        }

        public async Task<LearnerCourseContentDto> GetCourseContentFromRepo(string courseId, CancellationToken token)
        {
            return await _repo.GetCourseContentFromRepo(courseId, _currentUserAccessor.LearningPathId,
                       PublishedStatus.Instance.Name, token) ??
                   throw new NotFoundException($"The course ({courseId}) does not exist");
        }

        public async Task<string> GetLastLectureId(string courseId, CancellationToken token)
        {
            return await _repo.GetLastLectureId(courseId, _currentUserAccessor.UserId, token);
        }
    }
}
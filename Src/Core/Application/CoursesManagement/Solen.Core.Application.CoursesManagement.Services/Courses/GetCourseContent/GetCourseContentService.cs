using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.CoursesManagement.Courses.Queries;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.CoursesManagement.Common;

namespace Solen.Core.Application.CoursesManagement.Services.Courses
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

        public async Task<CourseContentDto> GetCourseContent(string courseId, CancellationToken token)
        {
            return await _repo.GetCourseContent(courseId, _currentUserAccessor.OrganizationId, token) ??
                         throw new NotFoundException($"The course ({courseId}) does not exist");

        }

    }
}
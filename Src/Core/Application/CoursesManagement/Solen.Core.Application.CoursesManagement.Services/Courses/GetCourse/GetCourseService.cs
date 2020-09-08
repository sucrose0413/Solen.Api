using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.CoursesManagement.Courses.Queries;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.CoursesManagement.Common;

namespace Solen.Core.Application.CoursesManagement.Services.Courses
{
    public class GetCourseService : IGetCourseService
    {
        private readonly IGetCourseRepository _repo;
        private readonly ICurrentUserAccessor _currentUserAccessor;
        private readonly ICourseErrorsManager _courseErrorsManager;

        public GetCourseService(IGetCourseRepository repo, ICurrentUserAccessor currentUserAccessor,
            ICourseErrorsManager courseErrorsManager)
        {
            _repo = repo;
            _currentUserAccessor = currentUserAccessor;
            _courseErrorsManager = courseErrorsManager;
        }

        public async Task<CourseDto> GetCourse(string courseId, CancellationToken token)
        {
            return await _repo.GetCourse(courseId, _currentUserAccessor.OrganizationId, token) ??
                   throw new NotFoundException($"The course ({courseId}) does not exist");
        }

        public async Task<IEnumerable<CourseErrorDto>> GetCourseErrors(string courseId, CancellationToken token)
        {
            return await _courseErrorsManager.GetCourseErrors(courseId, token);
        }
    }
}
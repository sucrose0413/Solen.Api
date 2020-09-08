using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.CoursesManagement.Edit.Courses.Commands;
using Solen.Core.Application.CoursesManagement.Edit.Services.Exceptions;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Common.Security;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Services.Courses
{
    public class CoursesCommonService : ICoursesCommonService
    {
        private readonly ICoursesCommonRepository _repo;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public CoursesCommonService(ICoursesCommonRepository repo, ICurrentUserAccessor currentUserAccessor)
        {
            _repo = repo;
            _currentUserAccessor = currentUserAccessor;
        }

        public async Task<Course> GetCourseFromRepo(string courseId, CancellationToken token)
        {
            return await _repo.FindCourse(courseId, _currentUserAccessor.OrganizationId, token) ??
                   throw new NotFoundException(nameof(Course), courseId);
        }

        public void CheckCourseStatusForModification(Course course)
        {
            if (!course.IsEditable)
                throw new UnalterableCourseException();
        }

        public void UpdateCourseRepo(Course course)
        {
            _repo.UpdateCourse(course);
        }
    }
}
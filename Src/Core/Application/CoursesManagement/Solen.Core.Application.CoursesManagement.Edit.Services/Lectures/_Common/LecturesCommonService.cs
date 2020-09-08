using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.CoursesManagement.Edit.Lectures.Commands;
using Solen.Core.Application.CoursesManagement.Edit.Services.Exceptions;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Common.Security;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Services.Lectures
{
    public class LecturesCommonService : ILecturesCommonService
    {
        private readonly ILecturesCommonRepository _repo;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public LecturesCommonService(ILecturesCommonRepository repo, ICurrentUserAccessor currentUserAccessor)
        {
            _repo = repo;
            _currentUserAccessor = currentUserAccessor;
        }
        
        public async Task<Lecture> GetLectureFromRepo(string lectureId, CancellationToken token)
        {
            return await _repo.GetLectureWithCourse(lectureId, _currentUserAccessor.OrganizationId, token) ??
                   throw new NotFoundException(nameof(Lecture), lectureId);
        }

        public void CheckCourseStatusForModification(Lecture lecture)
        {
            if (!lecture.Module.Course.IsEditable)
                throw new UnalterableCourseException();
        }
    }
}
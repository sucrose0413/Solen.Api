using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.CoursesManagement.Edit.Courses.Commands;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.CoursesManagement.Common;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Courses.Enums.CourseStatuses;

namespace Solen.Core.Application.CoursesManagement.Edit.Services.Courses
{
    public class PublishCourseService : IPublishCourseService
    {
        private readonly IPublishCourseRepository _repo;
        private readonly ICurrentUserAccessor _currentUserAccessor;
        private readonly ICourseErrorsManager _courseErrorsManager;
        private readonly IDateTime _dateTime;

        public PublishCourseService(IPublishCourseRepository repo, ICurrentUserAccessor currentUserAccessor,
            ICourseErrorsManager courseErrorsManager, IDateTime dateTime)
        {
            _repo = repo;
            _currentUserAccessor = currentUserAccessor;
            _courseErrorsManager = courseErrorsManager;
            _dateTime = dateTime;
        }

        public async Task<Course> GetCourseWithDetailsFromRepo(string courseId, CancellationToken token)
        {
            return await _repo.GetCourseWithDetails(courseId, _currentUserAccessor.OrganizationId, token) ??
                   throw new NotFoundException(nameof(Course), courseId);
        }


        public async Task CheckCourseErrors(string courseId, CancellationToken token)
        {
            var errors = (await _courseErrorsManager.GetCourseErrors(courseId, token)).ToList();

            if (errors.Any())
                throw new AppBusinessException(errors.Select(x => x.Error));
            
        }

        public void ChangeTheCourseStatusToPublished(Course course)
        {
            course.ChangeCourseStatus(new PublishedStatus());
        }

        public void UpdatePublicationDate(Course course)
        {
            course.UpdatePublicationDate(_dateTime.Now);
        }

        public void UpdateCourseRepo(Course course)
        {
            _repo.UpdateCourse(course);
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Courses.Commands
{
    public interface IPublishCourseService
    {
        Task<Course> GetCourseWithDetailsFromRepo(string courseId, CancellationToken token);
        Task CheckCourseErrors(string courseId, CancellationToken token);
        void ChangeTheCourseStatusToPublished(Course course);
        void UpdatePublicationDate(Course course);
        void UpdateCourseRepo(Course course);
    }
}
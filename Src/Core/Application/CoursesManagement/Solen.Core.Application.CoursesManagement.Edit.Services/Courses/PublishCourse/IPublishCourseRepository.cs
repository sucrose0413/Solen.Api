using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Services.Courses
{
    public interface IPublishCourseRepository
    {
        Task<Course> GetCourseWithDetails(string courseId, string organizationId, CancellationToken token);
        void UpdateCourse(Course course);
    }
}
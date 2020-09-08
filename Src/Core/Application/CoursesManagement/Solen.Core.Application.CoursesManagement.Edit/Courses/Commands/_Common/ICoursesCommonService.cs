using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Courses.Commands
{
    public interface ICoursesCommonService
    {
        Task<Course> GetCourseFromRepo(string courseId, CancellationToken token);
        void CheckCourseStatusForModification(Course course);
        void UpdateCourseRepo(Course course);
    }
}
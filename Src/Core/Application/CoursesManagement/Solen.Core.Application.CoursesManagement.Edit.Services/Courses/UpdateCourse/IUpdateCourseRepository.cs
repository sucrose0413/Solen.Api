using System.Threading;
using System.Threading.Tasks;

namespace Solen.Core.Application.CoursesManagement.Edit.Services.Courses
{
    public interface IUpdateCourseRepository
    {
        Task RemoveCourseSkills(string courseId, CancellationToken token);
    }
}
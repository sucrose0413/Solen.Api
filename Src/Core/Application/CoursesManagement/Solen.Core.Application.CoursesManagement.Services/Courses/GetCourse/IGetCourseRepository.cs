using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.CoursesManagement.Common;


namespace Solen.Core.Application.CoursesManagement.Services.Courses
{
    public interface IGetCourseRepository
    {
        Task<CourseDto> GetCourse(string courseId, string organizationId, CancellationToken token);
    }
}

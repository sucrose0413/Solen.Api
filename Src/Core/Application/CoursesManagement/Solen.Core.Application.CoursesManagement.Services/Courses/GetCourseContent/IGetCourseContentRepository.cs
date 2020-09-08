using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.CoursesManagement.Common;

namespace Solen.Core.Application.CoursesManagement.Services.Courses
{
    public interface IGetCourseContentRepository
    {
        Task<CourseContentDto> GetCourseContent(string courseId, string organizationId, CancellationToken token);
    }
}
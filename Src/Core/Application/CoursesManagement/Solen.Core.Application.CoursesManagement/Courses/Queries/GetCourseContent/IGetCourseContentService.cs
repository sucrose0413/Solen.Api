using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.CoursesManagement.Common;


namespace Solen.Core.Application.CoursesManagement.Courses.Queries
{
    public interface IGetCourseContentService
    {
        Task<CourseContentDto> GetCourseContent(string courseId, CancellationToken token);
    }
}
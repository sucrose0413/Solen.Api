using System.Threading;
using System.Threading.Tasks;

namespace Solen.Core.Application.Dashboard.Queries
{
    public interface IGetCoursesInfoService
    {
        Task<LastCreatedCourseDto> GetLastCreatedCourse(CancellationToken token);
        Task<LastPublishedCourseDto> GetLastPublishedCourse(CancellationToken token);
        Task<int> GetCourseCount(CancellationToken token);
    }
}
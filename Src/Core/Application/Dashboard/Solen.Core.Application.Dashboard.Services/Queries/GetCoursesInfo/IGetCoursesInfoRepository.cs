using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Dashboard.Queries;

namespace Solen.Core.Application.Dashboard.Services.Queries
{
    public interface IGetCoursesInfoRepository
    {
        Task<LastCreatedCourseDto> GetLastCreatedCourse(string organizationId, CancellationToken token);
        Task<LastPublishedCourseDto> GetLastPublishedCourse(string organizationId, string publishedStatus, CancellationToken token);
        Task<int> GetCourseCount(string organizationId, CancellationToken token);
    }
}
using System.Threading;
using System.Threading.Tasks;

namespace Solen.Core.Application.Learning.Queries
{
    public interface IGetCourseOverviewService
    {
        Task<LearnerCourseOverviewDto> GetCourseOverview(string courseId, CancellationToken token);
    }
}
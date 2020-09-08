using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Learning.Queries;

namespace Solen.Core.Application.Learning.Services.Queries
{
    public interface IGetCourseOverviewRepository
    {
        Task<LearnerCourseOverviewDto> GetCourseOverview(string courseId, string learningPathId,
            string publishedStatus, CancellationToken token);
    }
}
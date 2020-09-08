using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.Dashboard.Queries
{
    public interface IGetLearnerInfoService
    {
        Task<Course> GetLastCourse(CancellationToken token);
        Task<LearnerLastCourseProgressDto> GetLastCourseProgress(Course lastCourse, CancellationToken token);
    }
}
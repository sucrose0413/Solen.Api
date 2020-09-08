using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.Dashboard.Services.Queries
{
    public interface IGetLearnerInfoRepository
    {
        Task<Course> GetLastAccessedCourse(string learnerId, CancellationToken token);
        Task<Course> GetLearningPathFirstCourse(string learningPathId, CancellationToken token);
        Task<int> GetCourseTotalDuration(string courseId, CancellationToken token);
        Task<int> GetLearnerCompletedDuration(string learnerId, string courseId, CancellationToken token);
    }
}
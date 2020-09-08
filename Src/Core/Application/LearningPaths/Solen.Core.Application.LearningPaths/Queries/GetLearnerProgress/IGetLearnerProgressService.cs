using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Identity.Entities;

namespace Solen.Core.Application.LearningPaths.Queries
{
    public interface IGetLearnerProgressService
    {
        Task<User> GetLearner(string learnerId, CancellationToken token);
        Task<LearnerCompletedCoursesDto> GetLearnerCompletedCourses(User learner, CancellationToken token);
    }
}
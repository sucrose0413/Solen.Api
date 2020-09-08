using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Solen.Core.Application.LearningPaths.Services.Queries
{
    public interface IGetLearnerProgressRepository
    {
        Task<IList<LearningPathCourseDto>> GetLearningPathPublishedCourses(string learningPathId, string publishedStatus,
            CancellationToken token);

        Task<int> GetLearnerCompletedLectures(string learnerId, string courseId, string publishedStatus, CancellationToken token);
    }
}
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.LearningPaths.Commands
{
    public interface IRemoveCourseFromLearningPathService
    {
        Task<LearningPathCourse> GetLearningPathCourseFromRepo(string learningPathId, string courseId,
            CancellationToken token);

        void RemoveLearningPathCourseFromRepo(LearningPathCourse learningPathCourse);
    }
}
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.LearningPaths.Services.Commands
{
    public interface IRemoveCourseFromLearningPathRepository
    {
        Task<LearningPathCourse> GetLearningPathCourse(string learningPathId, string courseId,
            CancellationToken token);

        void RemoveLearningPathCourse(LearningPathCourse learningPathCourse);
    }
}
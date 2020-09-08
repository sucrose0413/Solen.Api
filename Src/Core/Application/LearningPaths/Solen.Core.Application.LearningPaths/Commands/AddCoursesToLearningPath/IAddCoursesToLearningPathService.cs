using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.LearningPaths.Commands
{
    public interface IAddCoursesToLearningPathService
    {
        Task<LearningPath> GetLearningPathFromRepo(string learningPathId, CancellationToken token);
        Task<bool> DoesCourseExist(string courseId, CancellationToken token);
        void AddCourseToLearningPath(LearningPath learningPath, string courseId);
        void UpdateLearningPathRepo(LearningPath learningPath);
    }
}
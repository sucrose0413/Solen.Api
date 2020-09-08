using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.LearningPaths.Services.Commands
{
    public interface IAddCoursesToLearningPathRepository
    {
        Task<LearningPath> GetLearningPathWithCourses(string learningPathId, string organizationId,
            CancellationToken token);

        Task<bool> DoesCourseExist(string courseId, string organizationId, CancellationToken token);
        void UpdateLearningPath(LearningPath learningPath);
    }
}
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Services.LearningPaths
{
    public interface IUpdateCourseLearningPathsRepository
    {
        Task<Course> GetCourseWithLearningPaths(string courseId, string organizationId, CancellationToken token);
        Task<int?> GetLearningPathLastOrder(string learningPathId, CancellationToken token);
        Task<bool> DoesLearningPathExist(string learningPathId, string organizationId, CancellationToken token);
        void UpdateCourseLearningPaths(Course course);
    }
}
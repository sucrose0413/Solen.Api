using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.LearningPaths.Commands;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.LearningPaths.Services.Commands
{
    public class RemoveCourseFromLearningPathService : IRemoveCourseFromLearningPathService
    {
        private readonly IRemoveCourseFromLearningPathRepository _repo;

        public RemoveCourseFromLearningPathService(IRemoveCourseFromLearningPathRepository repo)
        {
            _repo = repo;
        }

        public async Task<LearningPathCourse> GetLearningPathCourseFromRepo(string learningPathId, string courseId,
            CancellationToken token)
        {
            return await _repo.GetLearningPathCourse(learningPathId, courseId, token);
        }

        public void RemoveLearningPathCourseFromRepo(LearningPathCourse learningPathCourse)
        {
            _repo.RemoveLearningPathCourse(learningPathCourse);
        }
    }
}
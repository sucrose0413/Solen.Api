using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.LearningPaths.Commands;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.LearningPaths.Services.Commands
{
    public class AddCoursesToLearningPathService : IAddCoursesToLearningPathService
    {
        private readonly IAddCoursesToLearningPathRepository _repo;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public AddCoursesToLearningPathService(IAddCoursesToLearningPathRepository repo,
            ICurrentUserAccessor currentUserAccessor)
        {
            _repo = repo;
            _currentUserAccessor = currentUserAccessor;
        }

        public async Task<LearningPath> GetLearningPathFromRepo(string learningPathId, CancellationToken token)
        {
            return await _repo.GetLearningPathWithCourses(learningPathId, _currentUserAccessor.OrganizationId, token) ??
                   throw new NotFoundException($"The Learning path ({learningPathId}) does not exist");
        }

        public async Task<bool> DoesCourseExist(string courseId, CancellationToken token)
        {
            return await _repo.DoesCourseExist(courseId, _currentUserAccessor.OrganizationId, token);
        }

        public void AddCourseToLearningPath(LearningPath learningPath, string courseId)
        {
            learningPath.AddCourse(courseId);
        }

        public void UpdateLearningPathRepo(LearningPath learningPath)
        {
            _repo.UpdateLearningPath(learningPath);
        }
    }
}
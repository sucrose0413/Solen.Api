using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.LearningPaths.Commands;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.LearningPaths.Services.Commands
{
    public class UpdateLearningPathService : IUpdateLearningPathService
    {
        private readonly IUpdateLearningPathRepository _repo;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public UpdateLearningPathService(IUpdateLearningPathRepository repo, ICurrentUserAccessor currentUserAccessor)
        {
            _repo = repo;
            _currentUserAccessor = currentUserAccessor;
        }

        public async Task<LearningPath> GetLearningPath(string learningPathId, CancellationToken token)
        {
            return await _repo.GetLearningPath(learningPathId, _currentUserAccessor.OrganizationId, token) ??
                   throw new NotFoundException($"The Learning Path ({learningPathId}) does not exist");
        }

        public void UpdateName(LearningPath learningPath, string name)
        {
            learningPath.UpdateName(name);
        }

        public void UpdateDescription(LearningPath learningPath, string description)
        {
            learningPath.UpdateDescription(description);
        }

        public void UpdateLearningPathRepo(LearningPath learningPath)
        {
            _repo.UpdateLearningPath(learningPath);
        }
    }
}
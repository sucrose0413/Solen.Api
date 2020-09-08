using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.LearningPaths.Commands;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.LearningPaths.Services.Commands
{
    public class CreateLearningPathService : ICreateLearningPathService
    {
        private readonly ICreateLearningPathRepository _repo;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public CreateLearningPathService(ICreateLearningPathRepository repo, ICurrentUserAccessor currentUserAccessor)
        {
            _repo = repo;
            _currentUserAccessor = currentUserAccessor;
        }

        public LearningPath CreateLearningPath(string name)
        {
            return new LearningPath(name, _currentUserAccessor.OrganizationId);
        }

        public async Task AddLearningPathToRepo(LearningPath learningPath, CancellationToken token)
        {
            await _repo.AddLearningPath(learningPath, token);
        }
    }
}
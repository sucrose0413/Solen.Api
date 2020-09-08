using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Common.Identity;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Users.Commands;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Identity.Entities;

namespace Solen.Core.Application.Users.Services.Commands
{
    public class UpdateUserLearningPathService : IUpdateUserLearningPathService
    {
        private readonly IUpdateUserLearningPathRepository _repo;
        private readonly IUserManager _userManager;

        public UpdateUserLearningPathService(IUpdateUserLearningPathRepository repo, IUserManager userManager)
        {
            _repo = repo;
            _userManager = userManager;
        }

        public async Task<User> GetUserFromRepo(string userId, CancellationToken token)
        {
            return await _userManager.FindByIdAsync(userId) ??
                   throw new NotFoundException($"The user ({userId}) not found");
        }

        public async Task<LearningPath> GetLearningPath(string learningPathId, CancellationToken token)
        {
            return await _repo.GetLearningPath(learningPathId, token) ??
                   throw new NotFoundException($"The Learning Path ({learningPathId}) not found");
        }

        public void UpdateUserLearningPath(User user, LearningPath learningPath)
        {
            user.UpdateLearningPath(learningPath);
        }

        public void UpdateUserRepo(User user)
        {
            _userManager.UpdateUser(user);
        }
    }
}
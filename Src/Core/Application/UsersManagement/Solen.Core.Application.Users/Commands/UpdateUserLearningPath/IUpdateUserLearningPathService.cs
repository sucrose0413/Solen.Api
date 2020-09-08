using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Identity.Entities;

namespace Solen.Core.Application.Users.Commands
{
    public interface IUpdateUserLearningPathService
    {
        Task<User> GetUserFromRepo(string userId, CancellationToken token);
        Task<LearningPath> GetLearningPath(string learningPathId, CancellationToken token);
        void UpdateUserLearningPath(User user, LearningPath learningPath);
        void UpdateUserRepo(User user);
    }
}
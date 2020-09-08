using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Identity.Entities;

namespace Solen.Core.Application.Users.Commands
{
    public interface IInviteMembersService
    {
        Task CheckEmailExistence(string email);
        Task<LearningPath> GetLearningPathFromRepo(string learningPathId, CancellationToken token);
        List<User> CreateUsers(IEnumerable<string> emails);
        void SetTheInviteeToUser(User user);
        void SetTheInvitationTokenToUser(User user);
        void UpdateUserLearningPath(User user, LearningPath learningPath);
        void AddLearnerRoleToUser(User user);
        Task AddUserToRepo(User user);
    }
}
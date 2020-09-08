using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Identity.Entities;

namespace Solen.Core.Application.UserProfile.Commands
{
    public interface IUpdateCurrentUserPasswordService
    {
        Task<User> GetCurrentUser(CancellationToken token);
        void UpdateCurrentUserPassword(User user, string password);
        void UpdateCurrentUserRepo(User user);
    }
}
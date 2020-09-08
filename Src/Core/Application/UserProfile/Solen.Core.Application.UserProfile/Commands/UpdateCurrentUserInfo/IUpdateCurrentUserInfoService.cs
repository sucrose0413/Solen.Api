using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Identity.Entities;

namespace Solen.Core.Application.UserProfile.Commands
{
    public interface IUpdateCurrentUserInfoService
    {
        Task<User> GetCurrentUser(CancellationToken token);
        void UpdateCurrentUserName(User user, string userName);
        void UpdateCurrentUserRepo(User user);
    }
}
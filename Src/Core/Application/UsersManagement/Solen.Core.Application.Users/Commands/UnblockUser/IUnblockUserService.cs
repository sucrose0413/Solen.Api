using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Identity.Entities;

namespace Solen.Core.Application.Users.Commands
{
    public interface IUnblockUserService
    {
        Task<User> GetUser(string userId, CancellationToken token);
        void UnblockUser(User user);
        void UpdateUser(User user);
    }
}
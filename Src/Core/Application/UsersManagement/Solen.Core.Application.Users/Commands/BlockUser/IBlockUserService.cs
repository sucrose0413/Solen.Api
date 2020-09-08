using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Identity.Entities;

namespace Solen.Core.Application.Users.Commands
{
    public interface IBlockUserService
    {
        void CheckIfTheUserToBlockIsTheCurrentUser(string userId);
        Task<User> GetUser(string userId, CancellationToken token);
        void BlockUser(User user);
        void UpdateUser(User user);
    }
}
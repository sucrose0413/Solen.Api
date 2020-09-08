using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Identity.Entities;

namespace Solen.Core.Application.Auth.Queries
{
    public interface IGetCurrentLoggedUserService
    {
        Task<User> GetCurrentUser(CancellationToken token);
    }
}
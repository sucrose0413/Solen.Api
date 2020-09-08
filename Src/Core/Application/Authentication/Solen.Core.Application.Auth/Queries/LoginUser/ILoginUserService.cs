using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Identity.Entities;

namespace Solen.Core.Application.Auth.Queries
{
    public interface ILoginUserService
    {
        Task<User> GetUserByEmail(string email, CancellationToken token);
        void CheckIfPasswordIsInvalid(User user, string password);
    }
}
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Identity.Entities;

namespace Solen.Core.Application.Auth.Commands
{
    public interface IForgotPasswordService
    {
        Task<User> GetUserFromRepo(string email, CancellationToken token);
        void SetUserPasswordToken(User user);
        void UpdateUserRepo(User user);
        Task PublishPasswordTokenSetEvent(User user, CancellationToken token);
    }
}
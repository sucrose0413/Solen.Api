using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Identity.Entities;

namespace Solen.Core.Application.Auth.Commands
{
    public interface IResetPasswordService
    {
        Task<User> GetUserByPasswordToken(string passwordToken, CancellationToken token);
        void UpdateUserPassword(User user, string password);
        void InitUserPasswordToken(User user);
        void UpdateUserRepo(User user);
    }
}
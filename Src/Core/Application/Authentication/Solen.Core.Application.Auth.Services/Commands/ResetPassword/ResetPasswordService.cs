using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Auth.Commands;
using Solen.Core.Application.Common.Identity;
using Solen.Core.Application.Exceptions;
using Solen.Core.Domain.Identity.Entities;

namespace Solen.Core.Application.Auth.Services.Commands
{
    public class ResetPasswordService : IResetPasswordService
    {
        private readonly IUserManager _userManager;

        public ResetPasswordService(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> GetUserByPasswordToken(string passwordToken, CancellationToken token)
        {
            return await _userManager.FindByPasswordTokenAsync(passwordToken) ??
                   throw new NotFoundException("invalid token");
        }

        public void UpdateUserPassword(User user, string password)
        {
            _userManager.UpdatePassword(user, password);
        }

        public void InitUserPasswordToken(User user)
        {
            user.InitPasswordToken();
        }

        public void UpdateUserRepo(User user)
        {
            _userManager.UpdateUser(user);
        }
    }
}
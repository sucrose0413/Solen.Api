using System.Threading.Tasks;
using Solen.Core.Application.Common.Identity;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.SigningUp.Users.Commands;
using Solen.Core.Domain.Identity.Entities;

namespace Solen.Core.Application.SigningUp.Services.Users.Commands
{
    public class CompleteUserSigningUpService : ICompleteUserSigningUpService
    {
        private readonly IUserManager _userManager;

        public CompleteUserSigningUpService(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> GetUserByInvitationToken(string invitationToken)
        {
            return await _userManager.FindByInvitationTokenAsync(invitationToken) ??
                   throw new NotFoundException("token invalid or not found");
        }

        public void UpdateUserName(User user, string userName)
        {
            user.UpdateUserName(userName);
        }

        public void ValidateUserInscription(User user, string password)
        {
            _userManager.UpdatePassword(user, password);
            _userManager.ValidateUserInscription(user);
        }

        public void UpdateUserRepo(User user)
        {
            _userManager.UpdateUser(user);
        }
    }
}
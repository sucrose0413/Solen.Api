using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Common.Identity;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Users.Commands;
using Solen.Core.Domain.Identity.Entities;
using Solen.Core.Domain.Identity.Enums.UserStatuses;

namespace Solen.Core.Application.Users.Services.Commands
{
    public class UnblockUserService : IUnblockUserService
    {
        private readonly IUserManager _userManager;

        public UnblockUserService(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> GetUser(string userId, CancellationToken token)
        {
            return await _userManager.FindByIdAsync(userId) ??
                   throw new NotFoundException($"The user ({userId}) not found");
        }

        public void UnblockUser(User user)
        {
            user.ChangeUserStatus(ActiveStatus.Instance);
        }

        public void UpdateUser(User user)
        {
           _userManager.UpdateUser(user);
        }
    }
}
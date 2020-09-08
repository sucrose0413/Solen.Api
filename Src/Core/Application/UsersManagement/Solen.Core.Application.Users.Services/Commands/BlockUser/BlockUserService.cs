using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Common.Identity;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Users.Commands;
using Solen.Core.Domain.Identity.Entities;
using Solen.Core.Domain.Identity.Enums;
using Solen.Core.Domain.Identity.Enums.UserStatuses;

namespace Solen.Core.Application.Users.Services.Commands
{
    public class BlockUserService : IBlockUserService
    {
        private readonly IUserManager _userManager;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public BlockUserService(IUserManager userManager, ICurrentUserAccessor currentUserAccessor)
        {
            _userManager = userManager;
            _currentUserAccessor = currentUserAccessor;
        }

        public void CheckIfTheUserToBlockIsTheCurrentUser(string userId)
        {
            if (userId == _currentUserAccessor.UserId)
                throw new SameUserBlockingException();
        }

        public async Task<User> GetUser(string userId, CancellationToken token)
        {
            return await _userManager.FindByIdAsync(userId) ??
                   throw new NotFoundException($"The user ({userId}) not found");
        }

        public void BlockUser(User user)
        {
            user.ChangeUserStatus(new BlockedStatus());
        }

        public void UpdateUser(User user)
        {
            _userManager.UpdateUser(user);
        }
    }
}
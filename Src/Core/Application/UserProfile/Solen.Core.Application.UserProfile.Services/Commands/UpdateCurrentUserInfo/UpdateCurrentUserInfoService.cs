using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Common.Identity;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.UserProfile.Commands;
using Solen.Core.Domain.Identity.Entities;

namespace Solen.Core.Application.UserProfile.Services.Commands
{
    public class UpdateCurrentUserInfoService : IUpdateCurrentUserInfoService
    {
        private readonly IUserManager _userManager;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public UpdateCurrentUserInfoService(IUserManager userManager, ICurrentUserAccessor currentUserAccessor)
        {
            _userManager = userManager;
            _currentUserAccessor = currentUserAccessor;
        }

        public async Task<User> GetCurrentUser(CancellationToken token)
        {
            return await _userManager.FindByIdAsync(_currentUserAccessor.UserId);
        }

        public void UpdateCurrentUserName(User user, string userName)
        {
            user.UpdateUserName(userName);
        }

        public void UpdateCurrentUserRepo(User user)
        {
            _userManager.UpdateUser(user);
        }
    }
}
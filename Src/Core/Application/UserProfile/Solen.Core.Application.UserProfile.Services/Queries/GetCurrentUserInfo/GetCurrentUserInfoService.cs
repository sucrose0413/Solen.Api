using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Common.Identity;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.UserProfile.Queries;

namespace Solen.Core.Application.UserProfile.Services.Queries
{
    public class GetCurrentUserInfoService : IGetCurrentUserInfoService
    {
        private readonly IUserManager _userManager;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public GetCurrentUserInfoService(IUserManager userManager, ICurrentUserAccessor currentUserAccessor)
        {
            _userManager = userManager;
            _currentUserAccessor = currentUserAccessor;
        }

        public async Task<UserForProfileDto> GetCurrentUserInfo(CancellationToken token)
        {
            var user = await _userManager.FindByIdAsync(_currentUserAccessor.UserId);

            return new UserForProfileDto
            {
                UserName = user.UserName,
                Email = user.Email,
                LearningPath = user.LearningPath?.Name,
                Roles = string.Join(",", user.UserRoles.Select(x => x.Role.Name))
            };
        }
    }
}
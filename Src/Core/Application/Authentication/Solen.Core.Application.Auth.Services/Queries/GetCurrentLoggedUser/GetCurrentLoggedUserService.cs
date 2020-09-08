using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Auth.Queries;
using Solen.Core.Application.Common.Identity;
using Solen.Core.Application.Common.Security;
using Solen.Core.Domain.Identity.Entities;

namespace Solen.Core.Application.Auth.Services.Queries
{
    public class GetCurrentLoggedUserService : IGetCurrentLoggedUserService
    {
        private readonly IUserManager _userManager;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public GetCurrentLoggedUserService(IUserManager userManager, ICurrentUserAccessor currentUserAccessor)
        {
            _userManager = userManager;
            _currentUserAccessor = currentUserAccessor;
        }

        public async Task<User> GetCurrentUser(CancellationToken token)
        {
            return await _userManager.FindByIdAsync(_currentUserAccessor.UserId);
        }
    }
}
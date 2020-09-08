using System;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Auth.Queries;
using Solen.Core.Application.Common.Identity;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Exceptions;
using Solen.Core.Domain.Identity.Entities;
using Solen.Core.Domain.Security.Entities;

namespace Solen.Core.Application.Auth.Services.Queries
{
    public class CommonService : ICommonService
    {
        private readonly ICommonRepository _repo;
        private readonly IUserManager _userManager;
        private readonly IDateTime _dateTime;
        private readonly ISecurityConfig _securityConfig;

        public CommonService(ICommonRepository repo, IUserManager userManager, IDateTime dateTime,
            ISecurityConfig securityConfig)
        {
            _repo = repo;
            _userManager = userManager;
            _dateTime = dateTime;
            _securityConfig = securityConfig;
        }

        public void CheckIfUserIsBlockedOrInactive(User user)
        {
            if (!_userManager.IsActiveUser(user))
                throw new LockedException("The user account is deactivated");
        }

        public string CreateUserToken(User user)
        {
            var claims = _userManager.CreateUserClaims(user);
            return _userManager.CreateUserToken(claims);
        }

        public LoggedUserDto CreateLoggedUser(User user)
        {
            return new LoggedUserDto
            {
                Id = user.Id,
                LearningPath = user.LearningPath?.Name,
                UserName = user.UserName
            };
        }

        public async Task RemoveAnyUserRefreshToken(User user, CancellationToken token)
        {
            await _repo.RemoveAnyUserRefreshToken(user.Id, token);
        }

        public RefreshToken CreateNewRefreshToken(User user)
        {
            var expiryTimeInDays = _securityConfig.GetRefreshTokenExpiryTimeInDays();
            var expiryTime = GetExpiryTime(expiryTimeInDays);

            return new RefreshToken(user, expiryTime);
        }

        public void AddNewRefreshTokenToRepo(RefreshToken newRefreshToken)
        {
            _repo.AddRefreshToken(newRefreshToken);
        }

        #region Private Methods

        private DateTime? GetExpiryTime(int expiryTimeInDays)
        {
            return DoesTokenNeverExpire(expiryTimeInDays)
                ? (DateTime?) null
                : _dateTime.UtcNow.AddDays(expiryTimeInDays);
        }

        private static bool DoesTokenNeverExpire(int expiryTimeInDays)
        {
            return expiryTimeInDays == 0;
        }

        #endregion
    }
}
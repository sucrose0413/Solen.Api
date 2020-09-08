using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Identity.Entities;
using Solen.Core.Domain.Security.Entities;

namespace Solen.Core.Application.Auth.Queries
{
    public interface ICommonService
    {
        void CheckIfUserIsBlockedOrInactive(User user);
        string CreateUserToken(User user);
        LoggedUserDto CreateLoggedUser(User user);
        Task RemoveAnyUserRefreshToken(User user, CancellationToken token);
        RefreshToken CreateNewRefreshToken(User user);
        void AddNewRefreshTokenToRepo(RefreshToken newRefreshToken);
    }
}
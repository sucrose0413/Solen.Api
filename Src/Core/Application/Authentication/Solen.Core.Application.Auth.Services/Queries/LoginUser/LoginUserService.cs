using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Auth.Queries;
using Solen.Core.Application.Common.Identity;
using Solen.Core.Application.Exceptions;
using Solen.Core.Domain.Identity.Entities;

namespace Solen.Core.Application.Auth.Services.Queries
{
    public class LoginUserService : ILoginUserService
    {
        private readonly IUserManager _userManager;

        public LoginUserService(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> GetUserByEmail(string email, CancellationToken token)
        {
            return await _userManager.FindByEmailAsync(email) ??
                   throw new UnauthorizedException("Invalid login or password");
        }

        public void CheckIfPasswordIsInvalid(User user, string password)
        {
            if (!_userManager.CheckPassword(user, password))
                throw new UnauthorizedException("Invalid login or password");
        }
    }
}
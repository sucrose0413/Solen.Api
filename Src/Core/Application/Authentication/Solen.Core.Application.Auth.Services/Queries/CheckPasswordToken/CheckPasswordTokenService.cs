using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Auth.Queries;
using Solen.Core.Application.Common.Identity;
using Solen.Core.Application.Exceptions;

namespace Solen.Core.Application.Auth.Services.Queries
{
    public class CheckPasswordTokenService : ICheckPasswordTokenService
    {
        private readonly IUserManager _userManager;

        public CheckPasswordTokenService(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task CheckPasswordToken(string passwordToken, CancellationToken token)
        {
            if (!await _userManager.DoesPasswordTokenExist(passwordToken))
                throw new NotFoundException("invalid token");
        }
    }
}
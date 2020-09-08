using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.SigningUp.Users.Queries;

namespace Solen.Core.Application.SigningUp.Services.Users.Queries
{
    public class CheckUserSigningUpTokenService : ICheckUserSigningUpTokenService
    {
        private readonly ICheckUserSigningUpTokenRepository _repo;

        public CheckUserSigningUpTokenService(ICheckUserSigningUpTokenRepository repo)
        {
            _repo = repo;
        }

        public async Task CheckSigningUpToken(string signingUpToken, CancellationToken token)
        {
            if (!await _repo.DoesSigningUpTokenExist(signingUpToken, token))
                throw new NotFoundException("invalid token");
        }
    }
}
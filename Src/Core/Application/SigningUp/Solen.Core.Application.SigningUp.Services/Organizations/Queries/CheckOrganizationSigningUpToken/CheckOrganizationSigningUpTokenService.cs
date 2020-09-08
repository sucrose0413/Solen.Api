using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.SigningUp.Organizations.Queries;

namespace Solen.Core.Application.SigningUp.Services.Organizations.Queries
{
    public class CheckOrganizationSigningUpTokenService : ICheckOrganizationSigningUpTokenService
    {
        private readonly ICheckOrganizationSigningUpTokenRepository _repo;

        public CheckOrganizationSigningUpTokenService(ICheckOrganizationSigningUpTokenRepository repo)
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
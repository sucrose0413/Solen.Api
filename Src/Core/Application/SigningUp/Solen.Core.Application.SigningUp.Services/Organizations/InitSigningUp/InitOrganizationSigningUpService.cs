using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Common.Identity;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.SigningUp.Organizations.Commands;
using Solen.Core.Domain.Subscription.Entities;

namespace Solen.Core.Application.SigningUp.Services.Organizations
{
    public class InitOrganizationSigningUpService : IInitOrganizationSigningUpService
    {
        private readonly IInitOrganizationSigningUpRepository _repo;
        private readonly IUserManager _userManager;
        private readonly IRandomTokenGenerator _tokenGenerator;
        private readonly ISecurityConfig _securityConfig;

        public InitOrganizationSigningUpService(IInitOrganizationSigningUpRepository repo, IUserManager userManager,
            IRandomTokenGenerator tokenGenerator, ISecurityConfig securityConfig)
        {
            _repo = repo;
            _userManager = userManager;
            _tokenGenerator = tokenGenerator;
            _securityConfig = securityConfig;
        }

        public void CheckIfSigningUpIsEnabled()
        {
            if (!_securityConfig.IsSigninUpEnabled)
                throw new SigningUpNotEnabledException();
        }

        public async Task CheckEmailExistence(string email)
        {
            if (await _userManager.DoesEmailExistAsync(email))
                throw new EmailAlreadyRegisteredException(email);
        }

        public OrganizationSigningUp InitOrganizationSigningUp(string email)
        {
            return new OrganizationSigningUp(email, _tokenGenerator.CreateToken());
        }

        public async Task AddOrganizationSigningUpToRepo(OrganizationSigningUp signingUp, CancellationToken token)
        {
            await _repo.AddOrganizationSigningUp(signingUp, token);
        }
    }
}
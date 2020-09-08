using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Subscription.Entities;

namespace Solen.Core.Application.SigningUp.Organizations.Commands
{
    public interface IInitOrganizationSigningUpService
    {
        void CheckIfSigningUpIsEnabled();
        Task CheckEmailExistence(string email);
        OrganizationSigningUp InitOrganizationSigningUp(string email);
        Task AddOrganizationSigningUpToRepo(OrganizationSigningUp signingUp, CancellationToken token);
    }
}
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Subscription.Entities;

namespace Solen.Core.Application.SigningUp.Services.Organizations
{
    public interface IInitOrganizationSigningUpRepository
    {
        Task AddOrganizationSigningUp(OrganizationSigningUp signingUp, CancellationToken token);
    }
}
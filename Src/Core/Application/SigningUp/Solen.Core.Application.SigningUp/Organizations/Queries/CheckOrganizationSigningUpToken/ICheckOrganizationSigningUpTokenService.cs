using System.Threading;
using System.Threading.Tasks;

namespace Solen.Core.Application.SigningUp.Organizations.Queries
{
    public interface ICheckOrganizationSigningUpTokenService
    {
        Task CheckSigningUpToken(string signingUpToken, CancellationToken token);
    }
}
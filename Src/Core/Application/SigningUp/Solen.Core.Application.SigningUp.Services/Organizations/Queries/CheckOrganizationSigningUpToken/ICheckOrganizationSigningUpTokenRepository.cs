using System.Threading;
using System.Threading.Tasks;

namespace Solen.Core.Application.SigningUp.Services.Organizations.Queries
{
    public interface ICheckOrganizationSigningUpTokenRepository
    {
        Task<bool> DoesSigningUpTokenExist(string signingUpToken, CancellationToken token);
    }
}
using System.Threading;
using System.Threading.Tasks;

namespace Solen.Core.Application.SigningUp.Users.Queries
{
    public interface ICheckUserSigningUpTokenService
    {
        Task CheckSigningUpToken(string signingUpToken, CancellationToken token);
    }
}
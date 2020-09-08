using System.Threading;
using System.Threading.Tasks;

namespace Solen.Core.Application.SigningUp.Services.Users.Queries
{
    public interface ICheckUserSigningUpTokenRepository
    {
        Task<bool> DoesSigningUpTokenExist(string signingUpToken, CancellationToken token);
    }
}
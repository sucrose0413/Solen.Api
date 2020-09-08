using System.Threading;
using System.Threading.Tasks;

namespace Solen.Core.Application.Auth.Queries
{
    public interface ICheckPasswordTokenService
    { 
        Task CheckPasswordToken(string passwordToken, CancellationToken token);
    }
}
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Security.Entities;

namespace Solen.Core.Application.Auth.Services.Queries
{
    public interface ICommonRepository
    {
        Task RemoveAnyUserRefreshToken(string userId, CancellationToken token);
        void AddRefreshToken(RefreshToken refreshToken);
    }
}
using System.Threading;
using System.Threading.Tasks;

namespace Solen.Core.Application.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<int> SaveAsync(CancellationToken cancellationToken);
    }
}
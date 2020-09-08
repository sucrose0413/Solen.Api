using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Resources.Entities;

namespace Solen.Core.Application.Common.Resources.Impl
{
    public interface IAppResourceManagerRepo
    {
        void AddResource(AppResource appResource);
        Task UpdateResource(AppResource appResource);
        void RemoveResource(AppResource appResource);
        Task<IList<AppResource>> GetResourcesToDelete(CancellationToken token);
    }
}
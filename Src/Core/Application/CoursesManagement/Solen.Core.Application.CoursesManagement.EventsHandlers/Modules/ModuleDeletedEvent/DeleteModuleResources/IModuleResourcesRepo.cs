using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Resources.Entities;

namespace Solen.Core.Application.CoursesManagement.EventsHandlers.Modules
{
    public interface IModuleResourcesRepo
    {
        Task<IList<AppResource>> GetModuleResources(string moduleId, CancellationToken token);
    }
}
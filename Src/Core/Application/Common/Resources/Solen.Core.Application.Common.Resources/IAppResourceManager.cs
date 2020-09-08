using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Resources.Entities;

namespace Solen.Core.Application.Common.Resources
{
    public interface IAppResourceManager
    {
        ResourceUploadResult UploadResource(ResourceToCreate resourceToCreate);
        AppResource CreateAppResource(string resourceId, ResourceToCreate resourceToCreate);
        void AddAppResourceToRepo(AppResource appResource);
        Task Delete(AppResource appResource, CancellationToken token);
    }
}
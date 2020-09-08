using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Common.Security;
using Solen.Core.Domain.Resources.Entities;

namespace Solen.Core.Application.Common.Resources.Impl
{
    public class AppResourceManager : IAppResourceManager
    {
        private readonly IAppResourceManagerRepo _repo;
        private readonly IResourceStorageManagerFactory _storageFactory;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public AppResourceManager(IAppResourceManagerRepo repo, IResourceStorageManagerFactory storageFactory,
            ICurrentUserAccessor currentUserAccessor)
        {
            _repo = repo;
            _storageFactory = storageFactory;
            _currentUserAccessor = currentUserAccessor;
        }


        public ResourceUploadResult UploadResource(ResourceToCreate resourceToCreate)
        {
            var resourceAccessor = _storageFactory.Create(resourceToCreate.ResourceType);
            return resourceAccessor.Add(resourceToCreate.File);
        }

        public AppResource CreateAppResource(string resourceId, ResourceToCreate resourceToCreate)
        {
            return new AppResource(resourceId, _currentUserAccessor.OrganizationId,
                _currentUserAccessor.Username, resourceToCreate.ResourceType, resourceToCreate.File.Length);
        }

        public void AddAppResourceToRepo(AppResource appResource)
        {
            _repo.AddResource(appResource);
        }

        public async Task Delete(AppResource appResource, CancellationToken token)
        {
            appResource.MarkToDelete();

            await _repo.UpdateResource(appResource);
        }
    }
}
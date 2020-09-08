using System.Collections.Generic;
using System.Linq;
using Solen.Core.Application.Common.Resources;
using Solen.Core.Application.Common.Resources.Impl;
using Solen.Core.Domain.Resources.Enums.ResourceTypes;

namespace Solen.Infrastructure.Resources
{
    public class ResourceStorageManagerFactory : IResourceStorageManagerFactory
    {
        private readonly IList<ResourceAccessor> _resourceAccessors;

        public ResourceStorageManagerFactory(IList<ResourceAccessor> resourceAccessors)
        {
            _resourceAccessors = resourceAccessors ?? new List<ResourceAccessor>();
        }

        public ResourceAccessor Create(ResourceType resourceType)
        {
            var resourceAccessor = _resourceAccessors.FirstOrDefault(x => x.ResourceType.Name == resourceType.Name);

            if (resourceAccessor == null)
                throw new ResourceAccessorNotFoundException(nameof(resourceAccessor));
            
            return resourceAccessor;
        }
    }
}
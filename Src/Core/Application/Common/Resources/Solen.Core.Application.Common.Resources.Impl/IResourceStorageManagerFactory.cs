using Solen.Core.Domain.Resources.Enums.ResourceTypes;

namespace Solen.Core.Application.Common.Resources.Impl
{
    public interface IResourceStorageManagerFactory
    {
        ResourceAccessor Create(ResourceType resourceType);
    }
}
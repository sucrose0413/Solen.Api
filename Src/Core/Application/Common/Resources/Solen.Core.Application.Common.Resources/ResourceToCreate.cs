using Solen.Core.Domain.Resources.Enums.ResourceTypes;

namespace Solen.Core.Application.Common.Resources
{
    public class ResourceToCreate
    {
        public ResourceToCreate(IResourceFile file, ResourceType resourceType)
        {
            File = file;
            ResourceType = resourceType;
        }

        public IResourceFile File { get; }
        public ResourceType ResourceType { get; }
    }
}
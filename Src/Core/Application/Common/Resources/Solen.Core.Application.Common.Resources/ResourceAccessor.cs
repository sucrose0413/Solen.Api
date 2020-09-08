using Solen.Core.Domain.Resources.Enums.ResourceTypes;

namespace Solen.Core.Application.Common.Resources
{
    public abstract class ResourceAccessor
    {
        public abstract ResourceUploadResult Add(IResourceFile file);
        public abstract bool Delete(string publicId);
        public abstract ResourceType ResourceType { get; }
    }
}
using Microsoft.Extensions.Options;
using Solen.Core.Domain.Resources.Enums.ResourceTypes;

namespace Solen.Infrastructure.Resources.LocalStorage
{
    public class LocalImageAccessor : LocalResourceAccessorBase
    {
        public LocalImageAccessor(IOptions<LocalStorageSettings> config) : base(config, GetResourcesFolder(config))
        {
        }
        
        public override ResourceType ResourceType => new ImageType();

        #region Private Methods

        private static string GetResourcesFolder(IOptions<LocalStorageSettings> config)
        {
           return $"{config.Value.ResourcesFolder}/{config.Value.ImagesFolder}";
        }

        #endregion
    }
}
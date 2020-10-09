using Microsoft.Extensions.Options;
using Solen.Core.Domain.Resources.Enums.ResourceTypes;

namespace Solen.Infrastructure.Resources.LocalStorage
{
    public class LocalVideoAccessor : LocalResourceAccessorBase
    {
        public LocalVideoAccessor(IOptions<LocalStorageSettings> config) : base(
            config, GetResourcesFolder(config))
        {
        }

        public override ResourceType ResourceType => VideoType.Instance;

        #region Private Methods

        private static string GetResourcesFolder(IOptions<LocalStorageSettings> config)
        {
            return $"{config.Value.ResourcesFolder}/{config.Value.VideosFolder}";
        }

        #endregion
    }
}
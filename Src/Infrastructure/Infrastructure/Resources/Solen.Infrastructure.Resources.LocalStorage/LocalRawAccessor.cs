using Microsoft.Extensions.Options;
using Solen.Core.Domain.Resources.Enums.ResourceTypes;

namespace Solen.Infrastructure.Resources.LocalStorage
{
    public class LocalRawAccessor : LocalResourceAccessorBase
    {
        public LocalRawAccessor(IOptions<LocalStorageSettings> config) : base(
            config, GetResourcesFolder(config))
        {
        }


        public override ResourceType ResourceType => new RawType();

        #region Private Methods

        private static string GetResourcesFolder(IOptions<LocalStorageSettings> config)
        {
            return $"{config.Value.ResourcesFolder}/{config.Value.RawFolder}";
        }

        #endregion
    }
}
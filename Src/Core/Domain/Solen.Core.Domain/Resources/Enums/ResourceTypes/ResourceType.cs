using Solen.Core.Domain.Common;

namespace Solen.Core.Domain.Resources.Enums.ResourceTypes
{
    public abstract class ResourceType : Enumeration
    {
        protected ResourceType(int value, string name) : base(value, name)
        {
        }
    }
}
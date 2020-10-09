namespace Solen.Core.Domain.Resources.Enums.ResourceTypes
{
    public class RawType : ResourceType
    {
        public static readonly RawType Instance = new RawType();
        public RawType() : base(3, "Raw")
        {
        }
    }
}
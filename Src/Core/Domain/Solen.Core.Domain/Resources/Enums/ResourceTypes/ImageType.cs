namespace Solen.Core.Domain.Resources.Enums.ResourceTypes
{
    public class ImageType : ResourceType
    {
        public static readonly ImageType Instance = new ImageType();
        public ImageType() : base(1, "Image")
        {
        }
    }
}
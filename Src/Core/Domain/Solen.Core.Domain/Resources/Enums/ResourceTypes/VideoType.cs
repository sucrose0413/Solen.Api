namespace Solen.Core.Domain.Resources.Enums.ResourceTypes
{
    public class VideoType : ResourceType
    {
        public static readonly VideoType Instance = new VideoType();
        public VideoType() : base(2, "Video")
        {
        }
    }
}
namespace Solen.Core.Application.Common.Resources
{
    public class ResourceUploadResult
    {
        public ResourceUploadResult(string resourceId, string url)
        {
            ResourceId = resourceId;
            Url = url;
        }

        public string ResourceId { get; private set; }
        public string Url { get; private set; }
    }
}
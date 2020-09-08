using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using Solen.Core.Application.Common.Resources;


namespace Solen.Infrastructure.Resources.CloudinaryStorage
{
    public class ResourceAccessor 
    {
        private readonly Cloudinary _cloudinary;

        public ResourceAccessor(IOptions<CloudinarySettings> config)
        {
            var account = new Account
            {
                Cloud = config.Value.CloudName,
                ApiKey = config.Value.ApiKey,
                ApiSecret = config.Value.ApiSecret
            };

            _cloudinary = new Cloudinary(account);
        }


        public ResourceUploadResult AddVideo(IResourceFile file)
        {
            return UploadResource(file, ResourceType.Video);
        }

        public bool DeleteVideo(string publicId)
        {
            return Delete(publicId, ResourceType.Video);
        }

        public ResourceUploadResult AddImage(IResourceFile file)
        {
            return UploadResource(file, ResourceType.Image);
        }

        public bool DeleteImage(string publicId)
        {
            return Delete(publicId, ResourceType.Image);
        }

        public ResourceUploadResult AddRaw(IResourceFile file)
        {
            return UploadResource(file, ResourceType.Raw);
        }

        public bool DeleteRaw(string publicId)
        {
            return Delete(publicId, ResourceType.Raw);
        }

        private ResourceUploadResult UploadResource(IResourceFile file, ResourceType resourceType)
        {
            if (file == null || file.Length == 0)
                throw new UploadResourceException("null file");

            RawUploadResult uploadResult;
            var uploadParams = new RawUploadParams();
            switch (resourceType)
            {
                case ResourceType.Image:
                    uploadParams = new ImageUploadParams();
                    break;
                case ResourceType.Video:
                    uploadParams = new VideoUploadParams();
                    break;
            }

            using (var stream = file.OpenReadStream())
            {
                uploadParams.File = new FileDescription(file.FileName, stream);
                uploadResult = _cloudinary.Upload(uploadParams);
            }

            if (uploadResult.Error != null)
                throw new UploadResourceException(uploadResult.Error.Message);

            return new ResourceUploadResult(uploadResult.PublicId, uploadResult.SecureUrl.AbsoluteUri);
        }

        private bool Delete(string publicId, ResourceType resourceType)
        {
            var deleteParams = new DeletionParams(publicId) {ResourceType = resourceType};

            var result = _cloudinary.Destroy(deleteParams);

            return result.Result == "ok";
        }
    }
}
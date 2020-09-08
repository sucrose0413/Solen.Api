using Solen.Core.Application.Exceptions;


namespace Solen.Infrastructure.Resources.CloudinaryStorage
{
    public class UploadResourceException : AppTechnicalException
    {
        public UploadResourceException(string message) : base(message)
        {
        }
    }
}
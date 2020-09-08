using Solen.Core.Application.Exceptions;

namespace Solen.Infrastructure.Resources
{
    public class ResourceAccessorNotFoundException : AppTechnicalException
    {
        public ResourceAccessorNotFoundException(string message) : base(message)
        {
        }
    }
}
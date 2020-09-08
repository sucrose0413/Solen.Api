namespace Solen.Core.Application.Exceptions
{
    public class EmailAlreadyRegisteredException : AppBusinessException
    {
        public EmailAlreadyRegisteredException(string email)
            : base($"The Email ({email}) is already registered")
        {
        }
    }
}
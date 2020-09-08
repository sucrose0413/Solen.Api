using Solen.Core.Application.Exceptions;

namespace Solen.Core.Application.SigningUp.Services.Organizations
{
    public class SigningUpNotEnabledException : AppTechnicalException
    {
        public SigningUpNotEnabledException() : base("The Signing Up Is Not Enabled")
        {
        }
    }
}
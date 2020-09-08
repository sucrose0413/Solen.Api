using Solen.Core.Application.Exceptions;

namespace Solen.Core.Application.Users.Services.Commands
{
    public class SameUserBlockingException : AppBusinessException
    {
        public SameUserBlockingException() : base("The user can not block himself")
        {
        }
    }
}
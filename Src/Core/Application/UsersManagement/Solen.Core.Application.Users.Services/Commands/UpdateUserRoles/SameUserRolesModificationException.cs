using Solen.Core.Application.Exceptions;

namespace Solen.Core.Application.Users.Services.Commands
{
    public class SameUserRolesModificationException : AppBusinessException
    {
        public SameUserRolesModificationException() : base(
            "The same user can not modify its own roles")
        {
        }
    }
}
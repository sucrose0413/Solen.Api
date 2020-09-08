using Solen.Core.Domain.Common;

namespace Solen.Core.Domain.Identity.Enums.UserStatuses
{
    public abstract class UserStatus : Enumeration
    {
        protected UserStatus(int value, string name) : base(value, name)
        {
        }
    }
}
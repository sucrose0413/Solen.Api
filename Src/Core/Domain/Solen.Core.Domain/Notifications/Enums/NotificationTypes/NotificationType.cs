using Solen.Core.Domain.Common;

namespace Solen.Core.Domain.Notifications.Enums.NotificationTypes
{
    public abstract class NotificationType : Enumeration
    {
        protected NotificationType(int value, string name) : base(value, name)
        {
        }
    }
}
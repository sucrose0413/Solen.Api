using Solen.Core.Domain.Common;

namespace Solen.Core.Domain.Notifications.Enums.NotificationEvents
{
    public abstract class NotificationEvent : Enumeration
    {
        protected NotificationEvent(int value, string name, string description) : base(value, name)
        {
            Description = description;
        }
        
        public string Description { get; }
    }
}
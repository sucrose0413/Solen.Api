using System.Threading.Tasks;
using Solen.Core.Domain.Notifications.Enums.NotificationEvents;

namespace Solen.Core.Application.Common.Notifications
{
    public interface INotificationMessageHandler
    {
        Task Handle(RecipientContactInfo recipient, NotificationEvent @event, NotificationData data = null);
    }
}
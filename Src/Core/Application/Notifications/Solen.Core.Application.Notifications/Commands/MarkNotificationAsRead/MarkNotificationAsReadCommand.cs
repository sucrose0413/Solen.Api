using MediatR;

namespace Solen.Core.Application.Notifications.Commands
{
    public class MarkNotificationAsReadCommand : IRequest
    {
        public string NotificationId { get; set; }
    }
}
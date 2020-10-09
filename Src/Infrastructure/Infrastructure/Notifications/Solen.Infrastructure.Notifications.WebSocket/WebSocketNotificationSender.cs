using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Solen.Core.Application.Common.Notifications;
using Solen.Core.Application.Common.Notifications.Impl;
using Solen.Core.Application.UnitOfWork;
using Solen.Core.Domain.Notifications.Entities;
using Solen.Core.Domain.Notifications.Enums.NotificationTypes;

namespace Solen.Infrastructure.Notifications.WebSocket
{
    public class WebSocketNotificationSender : INotificationSender
    {
        private readonly IHubContext<ClientHub> _hubContext;
        private readonly INotificationsRepo _repo;
        private readonly IUnitOfWork _unitOfWork;

        public WebSocketNotificationSender(IHubContext<ClientHub> hubContext, INotificationsRepo repo,
            IUnitOfWork unitOfWork)
        {
            _hubContext = hubContext;
            _repo = repo;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(NotificationMessage message, RecipientContactInfo recipient)
        {
            if (message.Subject != null && message.Body != null)
            {
                _repo.AddNotification(new Notification(message.NotificationEvent, recipient.UserId, message.Subject,
                    message.Body));
                await _unitOfWork.SaveAsync(default);
            }

            await _hubContext.Clients.User(recipient.UserId)
                .SendAsync(message.NotificationEvent.Name, message.Subject, message.Body);
        }

        public NotificationType NotificationType => PushNotification.Instance;
    }
}
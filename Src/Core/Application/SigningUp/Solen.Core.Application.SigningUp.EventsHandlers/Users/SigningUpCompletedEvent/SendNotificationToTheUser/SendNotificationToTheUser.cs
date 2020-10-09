using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.Common.Notifications;
using Solen.Core.Application.SigningUp.Users.Commands;
using Solen.Core.Domain.Notifications.Enums.NotificationEvents;

namespace Solen.Core.Application.SigningUp.EventsHandlers.Users
{
    public class SendNotificationToTheUser : INotificationHandler<UserSigningUpCompletedEventNotification>
    {
        private readonly INotificationMessageHandler _notificationHandler;

        public SendNotificationToTheUser(INotificationMessageHandler notificationHandler)
        {
            _notificationHandler = notificationHandler;
        }

        public async Task Handle(UserSigningUpCompletedEventNotification @event, CancellationToken token)
        {
            var recipient = new RecipientContactInfo(null, @event.User.Email);
            var userInfo = new UserInfo(@event.User.UserName);

            await _notificationHandler.Handle(recipient, UserSigningUpCompletedEvent.Instance, userInfo);
        }
    }
}
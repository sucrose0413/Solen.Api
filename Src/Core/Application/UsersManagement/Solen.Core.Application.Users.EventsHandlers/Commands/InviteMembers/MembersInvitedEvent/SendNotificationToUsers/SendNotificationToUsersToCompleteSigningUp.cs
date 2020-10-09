using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using Solen.Core.Application.Common.Notifications;
using Solen.Core.Application.Users.Commands;
using Solen.Core.Domain.Notifications.Enums.NotificationEvents;

namespace Solen.Core.Application.Users.EventsHandlers.Commands
{
    public class SendNotificationToUsersToCompleteSigningUp : INotificationHandler<MembersInvitedEvent>
    {
        private readonly INotificationMessageHandler _notificationHandler;
        private readonly IOptions<CompleteUserSigningUpPageInfo> _signingUpPageOptions;

        public SendNotificationToUsersToCompleteSigningUp(INotificationMessageHandler notificationHandler,
            IOptions<CompleteUserSigningUpPageInfo> signingUpPage)
        {
            _notificationHandler = notificationHandler;
            _signingUpPageOptions = signingUpPage;
        }

        public async Task Handle(MembersInvitedEvent @event, CancellationToken token)
        {
            foreach (var user in @event.Users)
            {
                var recipient = new RecipientContactInfo(null, user.Email);
                var signingUpInfo = new SigningUpInfo(user.InvitedBy, GetLinkToCompleteSigningUp(user.InvitationToken));

                await _notificationHandler.Handle(recipient, UserSigningUpInitializedEvent.Instance,
                    signingUpInfo);
            }
        }

        private string GetLinkToCompleteSigningUp(string token)
        {
            if (SigningUpPageUrl == null || TokenParameterName == null)
                throw new ArgumentNullException("IOptions<CompleteUserSigningUpPageInfo>");

            var url = SigningUpPageUrl.EndsWith("/") ? SigningUpPageUrl : $"{SigningUpPageUrl}?";
            var tokenParameter = $"{TokenParameterName}={token}";
            return $"{url}{tokenParameter}";
        }
        
        private string SigningUpPageUrl => _signingUpPageOptions.Value.Url;
        private string TokenParameterName => _signingUpPageOptions.Value.TokenParameterName;
    }
}
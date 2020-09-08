using System;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.SigningUp.Organizations.Commands;
using MediatR;
using Microsoft.Extensions.Options;
using Solen.Core.Application.Common.Notifications;
using Solen.Core.Domain.Notifications.Enums.NotificationEvents;

namespace Solen.Core.Application.SigningUp.EventsHandlers.Organizations
{
    public class SendNotificationToCompleteSigningUp : INotificationHandler<SigningUpInitializedEvent>
    {
        private readonly INotificationMessageHandler _notificationHandler;
        private readonly IOptions<CompleteOrganizationSigningUpPageInfo> _signingUpPageOptions;

        public SendNotificationToCompleteSigningUp(INotificationMessageHandler notificationHandler,
            IOptions<CompleteOrganizationSigningUpPageInfo> signingUpPageOptions)
        {
            _notificationHandler = notificationHandler;
            _signingUpPageOptions = signingUpPageOptions;
        }

        public async Task Handle(SigningUpInitializedEvent @event, CancellationToken token)
        {
            var recipient = new RecipientContactInfo(null, @event.SigningUp.Email);
            var signingUpIfo = new SigningUpInfo(GetLinkToCompleteSigningUp(@event.SigningUp.Token));

            await _notificationHandler.Handle(recipient, new OrganizationSigningUpInitializedEvent(),
                signingUpIfo);
        }

        private string GetLinkToCompleteSigningUp(string token)
        {
            if (SigningUpPageUrl == null || TokenParameterName == null)
                throw new ArgumentNullException("IOptions<CompleteOrganizationSigningUpPageInfo>");

            var url = SigningUpPageUrl.EndsWith("/") ? SigningUpPageUrl : $"{SigningUpPageUrl}?";
            var tokenParameter = $"{TokenParameterName}={token}";
            return $"{url}{tokenParameter}";
        }
        
        private string SigningUpPageUrl => _signingUpPageOptions.Value.Url;
        private string TokenParameterName => _signingUpPageOptions.Value.TokenParameterName;
    }
}
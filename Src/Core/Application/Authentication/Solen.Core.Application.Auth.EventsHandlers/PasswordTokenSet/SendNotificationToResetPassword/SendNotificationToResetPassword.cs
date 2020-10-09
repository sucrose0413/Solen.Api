using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using Solen.Core.Application.Auth.Commands;
using Solen.Core.Application.Common.Notifications;
using Solen.Core.Domain.Notifications.Enums.NotificationEvents;

namespace Solen.Core.Application.Auth.EventsHandlers.PasswordTokenSet
{
    public class SendNotificationToResetPassword : INotificationHandler<PasswordTokenSetEvent>
    {
        private readonly INotificationMessageHandler _notificationHandler;
        private readonly IOptions<ResetPasswordPageInfo> _resetPageOptions;

        public SendNotificationToResetPassword(INotificationMessageHandler notificationHandler,
            IOptions<ResetPasswordPageInfo> resetPage)
        {
            _notificationHandler = notificationHandler;
            _resetPageOptions = resetPage;
        }

        public async Task Handle(PasswordTokenSetEvent @event, CancellationToken token)
        {
            var recipient = new RecipientContactInfo(@event.User.Id, @event.User.Email);
            var resetPasswordInfo = new ResetPasswordInfo(GetLinkToResetPassword(@event.User.PasswordToken));

            await _notificationHandler.Handle(recipient, PasswordForgottenEvent.Instance,
                resetPasswordInfo);
        }


        private string GetLinkToResetPassword(string token)
        {
            if (ResetPageUrl == null || TokenParameterName == null)
                throw new ArgumentNullException("IOptions<ResetPasswordPageInfo>");

            var url = ResetPageUrl.EndsWith("/") ? ResetPageUrl : $"{ResetPageUrl}?";
            var tokenParameter = $"{TokenParameterName}={token}";
            return $"{url}{tokenParameter}";
        }

        private string ResetPageUrl => _resetPageOptions.Value.Url;
        private string TokenParameterName => _resetPageOptions.Value.TokenParameterName;
    }
}
using Solen.Core.Application.Common.Notifications;

namespace Solen.Core.Application.SigningUp.EventsHandlers.Organizations
{
    public class SigningUpInfo : NotificationData
    {
        public SigningUpInfo(string linkToCompleteSigningUp)
        {
            LinkToCompleteSigningUp = linkToCompleteSigningUp;
        }

        public string LinkToCompleteSigningUp { get; }
    }
}
using Solen.Core.Application.Common.Notifications;

namespace Solen.Core.Application.Users.EventsHandlers.Commands
{
    public class SigningUpInfo : NotificationData
    {
        public SigningUpInfo(string invitedBy, string linkToCompleteSigningUp)
        {
            InvitedBy = invitedBy;
            LinkToCompleteSigningUp = linkToCompleteSigningUp;
        }

        public string InvitedBy { get; }
        public string LinkToCompleteSigningUp { get; }
    }
}
using Solen.Core.Application.Common.Notifications;

namespace Solen.Core.Application.Auth.EventsHandlers.PasswordTokenSet
{
    public class ResetPasswordInfo : NotificationData
    {
        public ResetPasswordInfo(string linkToResetPassword)
        {
            LinkToResetPassword = linkToResetPassword;
        }

        public string LinkToResetPassword { get; }
    }
}
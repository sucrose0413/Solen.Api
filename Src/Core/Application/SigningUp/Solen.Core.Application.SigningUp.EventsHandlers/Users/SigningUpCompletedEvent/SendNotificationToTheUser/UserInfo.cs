using Solen.Core.Application.Common.Notifications;

namespace Solen.Core.Application.SigningUp.EventsHandlers.Users
{
    public class UserInfo : NotificationData
    {
        public UserInfo(string userName)
        {
            UserName = userName;
        }

        public string UserName { get; }
    }
}
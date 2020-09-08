namespace Solen.Core.Application.Common.Notifications
{
    public class RecipientContactInfo
    {
        public RecipientContactInfo(string userId, string email)
        {
            UserId = userId;
            Email = email;
        }

        public string UserId { get; }
        public string Email { get; }
    }
}
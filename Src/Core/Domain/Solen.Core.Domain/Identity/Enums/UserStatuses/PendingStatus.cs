namespace Solen.Core.Domain.Identity.Enums.UserStatuses
{
    public class PendingStatus : UserStatus
    {
        public static readonly PendingStatus Instance = new PendingStatus();
        public PendingStatus() : base(1, "Pending")
        {
        }
    }
}
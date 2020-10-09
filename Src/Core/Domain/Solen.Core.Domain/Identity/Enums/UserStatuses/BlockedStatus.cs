namespace Solen.Core.Domain.Identity.Enums.UserStatuses
{
    public class BlockedStatus : UserStatus
    {
        public static readonly BlockedStatus Instance = new BlockedStatus();
        public BlockedStatus() : base(3, "Blocked")
        {
        }
    }
}
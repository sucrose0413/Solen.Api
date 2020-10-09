namespace Solen.Core.Domain.Identity.Enums.UserStatuses
{
    public class ActiveStatus : UserStatus
    {
        public static readonly ActiveStatus Instance = new ActiveStatus();
        public ActiveStatus() : base(2, "Active")
        {
        }
    }
}
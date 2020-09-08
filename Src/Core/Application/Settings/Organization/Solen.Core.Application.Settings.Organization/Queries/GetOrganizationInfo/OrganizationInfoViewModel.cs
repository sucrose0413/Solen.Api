namespace Solen.Core.Application.Settings.Organization.Queries
{
    public class OrganizationInfoViewModel
    {
        public string OrganizationName { get; set; }
        public string SubscriptionPlan { get; set; }
        public long MaxStorage { get; set; }
        public long CurrentStorage { get; set; }
        public int CurrentUsersCount { get; set; }
    }
}
namespace Solen.Core.Domain.Subscription.Entities
{
    public class SubscriptionPlan
    {
        private SubscriptionPlan()
        {
        }

        public SubscriptionPlan(string id, string name, long maxStorage, long maxFileSize, int maxUsers)
        {
            Id = id;
            Name = name;
            MaxStorage = maxStorage;
            MaxFileSize = maxFileSize;
            MaxUsers = maxUsers;
        }

        public string Id { get; private set; }
        public string Name { get; private set; }
        public long MaxStorage { get; private set; }
        public long MaxFileSize { get; private set; }
        public int MaxUsers { get; private set; }
    }
}
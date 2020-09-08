using System;
using Solen.Core.Domain.Subscription.Entities;

namespace Solen.Core.Domain.Common.Entities
{
    public class Organization
    {
        #region Constructors

        private Organization()
        {
        }

        public Organization(string name, string subscriptionPlanId)
        {
            Id = OrganizationNewId;
            Name = name;
            SubscriptionPlanId = subscriptionPlanId;
        }

        #endregion

        #region Public Properties & Methods

        public string Id { get; private set; }
        public string Name { get; private set; }
        
        public string SubscriptionPlanId { get; private set; }
        public SubscriptionPlan SubscriptionPlan { get; private set; }

        public virtual void UpdateName(string name)
        {
            Name = name;
        }

        #endregion
        
        #region Private Methods

        private static string OrganizationNewId => new Random().Next(0, 999999999).ToString("D9");

        #endregion
    }
}
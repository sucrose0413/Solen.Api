using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solen.Core.Domain.Subscription.Constants;
using Solen.Core.Domain.Subscription.Entities;

namespace Solen.Persistence.Configurations
{
    public class SubscriptionPlanConfiguration : IEntityTypeConfiguration<SubscriptionPlan>
    {
        public void Configure(EntityTypeBuilder<SubscriptionPlan> builder)
        {
            builder.Property(x => x.Id).HasMaxLength(127);

            builder.Property(c => c.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasData(
                new SubscriptionPlan(SubscriptionPlans.Free, "Free Plan", 5368709120 , 314572800, 20));
        }
    }
}
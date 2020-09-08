using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solen.Core.Domain.Common.Entities;


namespace Solen.Persistence.Configurations
{
    public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.Property(x => x.Id).HasMaxLength(127);

            builder.Property(m => m.Name)
                .HasMaxLength(60)
                .IsRequired();
            
            builder.HasOne(o => o.SubscriptionPlan)
                .WithMany()
                .HasForeignKey(o => o.SubscriptionPlanId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}

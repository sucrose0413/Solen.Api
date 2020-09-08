using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solen.Core.Domain.Subscription.Entities;

namespace Solen.Persistence.Configurations
{
    public class OrganizationSigningUpConfiguration : IEntityTypeConfiguration<OrganizationSigningUp>
    {
        public void Configure(EntityTypeBuilder<OrganizationSigningUp> builder)
        {
            builder.Property(x => x.Id).HasMaxLength(127);

            builder.Property(x => x.Email)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Token)
                .HasMaxLength(250)
                .IsRequired();
        }
    }
}
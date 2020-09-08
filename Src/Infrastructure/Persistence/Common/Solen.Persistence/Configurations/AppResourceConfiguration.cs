using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solen.Core.Domain.Resources.Entities;

namespace Solen.Persistence.Configurations
{
    public class AppResourceConfiguration : IEntityTypeConfiguration<AppResource>
    {
        public void Configure(EntityTypeBuilder<AppResource> builder)
        {
            builder.Property(x => x.Id).HasMaxLength(127);
            builder.Property(x => x.ResourceTypeName).HasMaxLength(50);
            builder.Property(x => x.CreatorName).HasMaxLength(100);
            builder.HasIndex(x => x.OrganizationId);
        }
    }
}

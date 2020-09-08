using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Persistence.Configurations
{
    public class LearningPathConfiguration : IEntityTypeConfiguration<LearningPath>
    {
        public void Configure(EntityTypeBuilder<LearningPath> builder)
        {
            builder.Property(x => x.Id).HasMaxLength(127);

            builder.Property(c => c.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.Description)
                .HasMaxLength(100);

            builder.HasOne(u => u.Organization)
                .WithMany()
                .HasForeignKey(u => u.OrganizationId)
                .IsRequired();
        }
    }
}
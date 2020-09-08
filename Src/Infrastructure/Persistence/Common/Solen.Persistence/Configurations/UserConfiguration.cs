using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solen.Core.Domain.Identity.Entities;

namespace Solen.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Id).HasMaxLength(127);

            builder.HasIndex(e => e.Email).IsUnique();

            builder.Property(e => e.Email)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.UserName)
                .HasMaxLength(30);

            builder.Property(e => e.InvitedBy)
                .HasMaxLength(50);

            builder.Property(c => c.UserStatusName)
                .HasMaxLength(50);

            builder.HasOne(u => u.Organization)
                .WithMany()
                .HasForeignKey(u => u.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            
            builder.HasOne(u => u.LearningPath)
                .WithMany(o => o.Learners)
                .HasForeignKey(u => u.LearningPathId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);
        }
    }
}
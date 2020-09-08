using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solen.Core.Domain.Notifications.Entities;

namespace Solen.Persistence.Configurations
{
    public class DisabledNotificationTemplateConfiguration : IEntityTypeConfiguration<DisabledNotificationTemplate>
    {
        public void Configure(EntityTypeBuilder<DisabledNotificationTemplate> builder)
        {
            builder.HasKey(x => new {x.OrganizationId, x.NotificationTemplateId});

            builder.HasOne(x => x.Organization)
                .WithMany()
                .HasForeignKey(x => x.OrganizationId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.NotificationTemplate)
                .WithMany()
                .HasForeignKey(x => x.NotificationTemplateId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
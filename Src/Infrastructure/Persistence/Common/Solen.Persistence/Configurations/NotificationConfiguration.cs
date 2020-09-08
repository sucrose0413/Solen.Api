using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solen.Core.Domain.Common;
using Solen.Core.Domain.Notifications.Entities;
using Solen.Core.Domain.Notifications.Enums.NotificationEvents;

namespace Solen.Persistence.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.Property(x => x.Id).HasMaxLength(127);

            builder.HasOne(x => x.Recipient)
                .WithMany()
                .HasForeignKey(x => x.RecipientId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.Subject).HasMaxLength(250);

            builder.Property(x => x.NotificationEvent)
                .HasConversion(t => t.ToString(),
                    v => Enumeration.GetAll<NotificationEvent>().Single(s => s.ToString() == v));
        }
    }
}

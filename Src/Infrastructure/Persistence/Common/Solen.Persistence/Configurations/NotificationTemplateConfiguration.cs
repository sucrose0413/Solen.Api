using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solen.Core.Domain.Notifications.Entities;
using Solen.Persistence.Data.NotificationTemplates;

namespace Solen.Persistence.Configurations
{
    public class NotificationTemplateConfiguration : IEntityTypeConfiguration<NotificationTemplate>
    {
        public void Configure(EntityTypeBuilder<NotificationTemplate> builder)
        {

            builder.Property(x => x.Id).HasMaxLength(127);

            builder.Property(x => x.TemplateSubject)
                .HasMaxLength(100);

            builder.Property(x => x.NotificationEventName)
                .HasMaxLength(50);

            builder.Property(x => x.TypeName)
                .HasMaxLength(50);

            builder.HasData(CoursePublishedEmailTemplateCreator.Create());
            builder.HasData(CoursePublishedPushTemplateCreator.Create());
            builder.HasData(OrgSigningUpInitializedEmailTemplateCreator.Create());
            builder.HasData(OrgSigningUpCompletedEmailTemplateCreator.Create());
            builder.HasData(PasswordForgottenEmailTemplateCreator.Create());
            builder.HasData(UserSigningUpCompletedEmailTemplateCreator.Create());
            builder.HasData(UserSigningUpInitializedEmailTemplateCreator.Create());
        }
    }
}
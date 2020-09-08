using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Persistence.Configurations
{
    public class ModuleConfiguration : IEntityTypeConfiguration<Module>
    {
        public void Configure(EntityTypeBuilder<Module> builder)
        {
            builder.Property(x => x.Id).HasMaxLength(127);

            builder.Property(m => m.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(m => m.CourseId)
                .IsRequired();

            builder.HasOne(m => m.Course)
                .WithMany(c => c.Modules)
                .OnDelete(deleteBehavior:DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}

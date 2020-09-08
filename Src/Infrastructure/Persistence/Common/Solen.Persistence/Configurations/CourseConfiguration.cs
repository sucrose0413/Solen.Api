using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solen.Core.Domain.Courses.Entities;


namespace Solen.Persistence.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(x => x.Id).HasMaxLength(127);

            builder.Property(c => c.Title)
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(c => c.Subtitle)
                .HasMaxLength(120);

            builder.Property(c => c.CreatorId)
                .IsRequired();

            builder.Property(c => c.CourseStatusName)
                .HasMaxLength(50);

            builder.HasOne(c => c.Creator)
                .WithMany(u => u.CreatedCourses)
                .OnDelete(DeleteBehavior.Restrict);
            
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Persistence.Configurations
{
    public class CourseResourceConfiguration : IEntityTypeConfiguration<CourseResource>
    {
        public void Configure(EntityTypeBuilder<CourseResource> builder)
        {
            builder.Property(x => x.CourseId).HasMaxLength(127);
            builder.Property(x => x.ModuleId).HasMaxLength(127);
            builder.Property(x => x.LectureId).HasMaxLength(127);

            builder.HasKey(x => new {x.CourseId, x.ModuleId, x.LectureId, x.ResourceId});
        }
    }
}
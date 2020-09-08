using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Persistence.Configurations
{
    public class LearnerCourseAccessTimeConfiguration : IEntityTypeConfiguration<LearnerCourseAccessTime>
    {
        public void Configure(EntityTypeBuilder<LearnerCourseAccessTime> builder)
        {

            builder.HasKey(x => new {x.LearnerId, x.CourseId});

            builder.HasOne(x => x.Course)
                .WithMany()
                .HasForeignKey(x => x.CourseId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Learner)
                .WithMany()
                .HasForeignKey(x => x.LearnerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
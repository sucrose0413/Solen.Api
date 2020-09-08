using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solen.Core.Domain.Courses.Entities;


namespace Solen.Persistence.Configurations
{
    public class LearningPathCourseConfiguration : IEntityTypeConfiguration<LearningPathCourse>
    {
        public void Configure(EntityTypeBuilder<LearningPathCourse> builder)
        {
            builder.HasKey(lc => new {lc.LearningPathId, lc.CourseId});

            builder.HasOne(lc => lc.LearningPath)
                .WithMany(l => l.LearningPathCourses)
                .HasForeignKey(lc => lc.LearningPathId);

            builder.HasOne(lc => lc.Course)
                .WithMany(c => c.CourseLearningPaths)
                .HasForeignKey(lc => lc.CourseId);
        }
    }
}
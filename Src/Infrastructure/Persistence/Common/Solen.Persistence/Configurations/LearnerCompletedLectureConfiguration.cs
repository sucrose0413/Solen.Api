using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Persistence.Configurations
{
    public class LearnerCompletedLectureConfiguration : IEntityTypeConfiguration<LearnerCompletedLecture>
    {
        public void Configure(EntityTypeBuilder<LearnerCompletedLecture> builder)
        {
            builder.HasKey(x => new { x.LearnerId, x.LectureId });

            builder.HasOne(x => x.Lecture)
                .WithMany()
                .HasForeignKey(x => x.LectureId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Learner)
                .WithMany(u => u.CompletedLectures)
                .HasForeignKey(x => x.LearnerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
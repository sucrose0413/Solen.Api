using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Persistence.Configurations
{
    public class LearnerLectureAccessHistoryConfiguration : IEntityTypeConfiguration<LearnerLectureAccessHistory>
    {
        public void Configure(EntityTypeBuilder<LearnerLectureAccessHistory> builder)
        {
            builder.HasOne(x => x.Lecture)
                .WithMany()
                .HasForeignKey(x => x.LectureId)
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
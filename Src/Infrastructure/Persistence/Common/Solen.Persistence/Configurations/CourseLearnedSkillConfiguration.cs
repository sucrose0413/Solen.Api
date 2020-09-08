using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Persistence.Configurations
{
    public class CourseLearnedSkillConfiguration : IEntityTypeConfiguration<CourseLearnedSkill>
    {
        public void Configure(EntityTypeBuilder<CourseLearnedSkill> builder)
        {
            builder.Property(x => x.Id).HasMaxLength(127);

            builder.Property(s => s.Name)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(s => s.CourseId)
                .IsRequired();

            builder.HasOne(s => s.Course)
                .WithMany(c => c.CourseLearnedSkills)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
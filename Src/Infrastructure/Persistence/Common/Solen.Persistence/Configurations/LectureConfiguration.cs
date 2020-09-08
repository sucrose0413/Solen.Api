using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solen.Core.Domain.Common;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Courses.Enums;


namespace Solen.Persistence.Configurations
{
    public class LectureConfiguration : IEntityTypeConfiguration<Lecture>
    {
        public void Configure(EntityTypeBuilder<Lecture> builder)
        {
            builder.Property(x => x.Id).HasMaxLength(127);

            builder.Property(l => l.Title)
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(l => l.ModuleId)
                .IsRequired();

            builder.Property(c => c.LectureTypeName)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
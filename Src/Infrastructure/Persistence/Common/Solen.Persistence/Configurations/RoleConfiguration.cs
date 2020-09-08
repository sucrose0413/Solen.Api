using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solen.Core.Domain.Identity.Entities;
using Solen.Core.Domain.Identity.Enums;

namespace Solen.Persistence.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(x => x.Id).HasMaxLength(127);

            builder.Property(c => c.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.Description)
                .HasMaxLength(250);

            builder.HasData(
                new Role(UserRoles.Admin, "Admin", "Has all controls and access."),
                new Role(UserRoles.Instructor, "Instructor", "Can create Learning Paths, create, publish and unpublish courses"),
                new Role(UserRoles.Learner, "Learner", "Can access to all courses belonging to his learning path"));
        }
    }
}
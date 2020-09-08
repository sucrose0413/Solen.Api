using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.Common.Security;
using Solen.Core.Domain.Common;
using Solen.Core.Domain.Common.Entities;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Identity.Entities;
using Solen.Core.Domain.Notifications.Entities;
using Solen.Core.Domain.Resources.Entities;
using Solen.Core.Domain.Security.Entities;
using Solen.Core.Domain.Subscription.Entities;

namespace Solen.Persistence
{
    public class SolenDbContext : DbContext
    {
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public SolenDbContext(DbContextOptions<SolenDbContext> options)
            : base(options)
        {
        }

        public SolenDbContext(
            DbContextOptions<SolenDbContext> options, ICurrentUserAccessor currentUserAccessor)
            : base(options)
        {
            _currentUserAccessor = currentUserAccessor;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SolenDbContext).Assembly);
        }

        public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
        public DbSet<OrganizationSigningUp> OrganizationSigningUps { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<ArticleLecture> ArticleLectures { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<VideoLecture> VideoLectures { get; set; }
        public DbSet<CourseLearnedSkill> CourseLearnedSkill { get; set; }
        public DbSet<CourseResource> CourseResources { get; set; }
        public DbSet<AppResource> AppResources { get; set; }
        public DbSet<NotificationTemplate> NotificationTemplates { get; set; }
        public DbSet<DisabledNotificationTemplate> DisabledNotificationTemplates { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<LearningPath> LearningPaths { get; set; }
        public DbSet<LearningPathCourse> LearningPathCourses { get; set; }
        public DbSet<LearnerCompletedLecture> LearnerCompletedLectures { get; set; }
        public DbSet<LearnerLectureAccessHistory> LearnerLectureAccessHistories { get; set; }
        public DbSet<LearnerCourseAccessTime> LearnerCourseAccessTimes { get; set; }


        // users and roles
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserAccessor.Username;
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.LastModifiedBy = entry.Entity.CreatedBy;
                        entry.Entity.LastModified = entry.Entity.Created;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserAccessor.Username;
                        entry.Entity.LastModified = DateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
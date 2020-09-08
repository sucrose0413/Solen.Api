using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.Common.Notifications;
using Solen.Core.Application.CoursesManagement.EventsHandlers.Courses;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Identity.Entities;

namespace Solen.Persistence.CoursesManagement.EventsHandlers.Courses
{
    public class SendNotificationsToCourseLearnersRepo : ISendNotificationsToCourseLearnersRepo
    {
        private readonly SolenDbContext _context;

        public SendNotificationsToCourseLearnersRepo(SolenDbContext context)
        {
            _context = context;
        }

        public async Task<CourseInfo> GetCourseInfo(string courseId, CancellationToken token)
        {
            return await _context.Courses
                .Where(c => c.Id == courseId)
                .Select(MapCourse())
                .FirstOrDefaultAsync(token);
        }

        public async Task<IEnumerable<RecipientContactInfo>> GetCourseLearners(string courseId, CancellationToken token)
        {
            return await _context.LearningPathCourses
                .Where(x => x.CourseId == courseId)
                .SelectMany(x => x.LearningPath.Learners)
                .Select(MapLearner())
                .ToListAsync(token);
        }

        #region Private Methods

        private static Expression<Func<Course, CourseInfo>> MapCourse()
        {
            return x => new CourseInfo(x.Title, x.Creator.UserName);
        }

        private static Expression<Func<User, RecipientContactInfo>> MapLearner()
        {
            return x => new RecipientContactInfo(x.Id, x.Email);
        }

        #endregion
    }
}
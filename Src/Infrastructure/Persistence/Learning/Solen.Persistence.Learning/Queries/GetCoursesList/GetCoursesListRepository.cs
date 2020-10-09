using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.Learning.Queries;
using Solen.Core.Application.Learning.Services.Queries;
using Solen.Core.Domain.Courses.Entities;
using Solen.Persistence.Extensions;

namespace Solen.Persistence.Learning.Queries
{
    public class GetCoursesListRepository : IGetCoursesListRepository
    {
        private readonly SolenDbContext _context;

        public GetCoursesListRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task<LearnerCoursesListResult> GetCoursesList(GetCoursesListQuery coursesQuery, string learnerId,
            string learningPathId, string publishedStatus, CancellationToken token)
        {
            IQueryable<Course> query;
            if (IsOrderedByLastAccess(coursesQuery.OrderBy))
            {
                query = OrderByLastAccess(learnerId, learningPathId, publishedStatus);
            }
            else
            {
                query = OrderByLearningPathOrder(learningPathId, publishedStatus);
            }

            var totalItems = await query.CountAsync(token);

            query = query.ApplyPaging(coursesQuery);

            var items = await query
                .Select(CourseMapping())
                .ToListAsync(token);

            var progress = await GetLearnerCourseProgress(learnerId, token, items);

            return new LearnerCoursesListResult(totalItems, items, progress);
        }

        #region Private Methods

        private static bool IsOrderedByLastAccess(int orderBy)
        {
            return orderBy == OrderByLastAccessed.Instance.Value;
        }

        private IQueryable<Course> OrderByLastAccess(string learnerId, string learningPathId, string publishedStatus)
        {
            var query = from lp in _context.LearningPathCourses
                join a in _context.LearnerCourseAccessTimes.Where(x => x.LearnerId == learnerId) on
                    lp.CourseId equals a.CourseId into h
                from access in h.DefaultIfEmpty()
                orderby access.AccessDate descending, lp.Order
                where lp.LearningPathId == learningPathId &&
                      lp.Course.CourseStatusName == publishedStatus
                select lp.Course;

            return query;
        }

        private IQueryable<Course> OrderByLearningPathOrder(string learningPathId, string publishedStatus)
        {
            var query = _context.LearningPathCourses
                .Where(cg =>
                    cg.LearningPathId == learningPathId &&
                    cg.Course.CourseStatusName == publishedStatus)
                .OrderBy(lp => lp.Order)
                .Select(cg => cg.Course);

            return query;
        }

        private async Task<List<LearnerCourseProgressDto>> GetLearnerCourseProgress(string learnerId,
            CancellationToken token, List<LearnerCourseDto> items)
        {
            var progress = new List<LearnerCourseProgressDto>();
            foreach (var course in items)
            {
                var duration = await _context.LearnerCompletedLectures.Where(x =>
                        x.LearnerId == learnerId && x.Lecture.Module.CourseId == course.Id)
                    .SumAsync(l => l.Lecture.Duration, token);

                progress.Add(new LearnerCourseProgressDto(course.Id, duration));
            }

            return progress;
        }


        private static Expression<Func<Course, LearnerCourseDto>> CourseMapping()
        {
            return course => new LearnerCourseDto
            {
                Id = course.Id,
                Title = course.Title,
                Subtitle = course.Subtitle,
                Creator = course.Creator.UserName,
                Description = course.Description,
                CreationDate = course.CreationDate,
                Duration = course.Modules.SelectMany(m => m.Lectures).Sum(l => l.Duration),
                LectureCount = course.Modules.SelectMany(m => m.Lectures).Count()
            };
        }

        #endregion
    }
}
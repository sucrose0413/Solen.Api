using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.CoursesManagement.Courses.Queries;
using Solen.Core.Application.CoursesManagement.Services.Courses;
using Solen.Core.Domain.Common;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Courses.Enums.CourseStatuses;
using Solen.Persistence.Extensions;

namespace Solen.Persistence.CoursesManagement.Courses
{
    public class GetCoursesListRepository : IGetCoursesListRepository
    {
        private readonly SolenDbContext _context;

        public GetCoursesListRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task<CoursesListResult> GetCoursesList(GetCoursesListQuery coursesQuery,
            string organizationId,
            CancellationToken token)
        {
            var query = _context.Courses.Where(c => c.Creator.OrganizationId == organizationId);

            query = ApplyAuthorFiltering(query, coursesQuery.AuthorId);

            query = ApplyLearningPathFiltering(query, coursesQuery.LearningPathId);

            query = ApplyStatusFiltering(query, coursesQuery.StatusId);

            query = ApplyOrdering(query, coursesQuery);

            var totalItems = await query.CountAsync(token);

            query = query.ApplyPaging(coursesQuery);

            var items = await query.Select(CourseMapping()).ToListAsync(token);

            return new CoursesListResult(totalItems, items);
        }

        #region Private Methods

        private static IQueryable<Course> ApplyAuthorFiltering(IQueryable<Course> query, string authorId)
        {
            if (!string.IsNullOrEmpty(authorId))
                query = query.Where(c => c.CreatorId == authorId);
            return query;
        }

        private static IQueryable<Course> ApplyLearningPathFiltering(IQueryable<Course> query, string learningPathId)
        {
            if (!string.IsNullOrEmpty(learningPathId))
                query = query.Where(c => c.CourseLearningPaths.Any(l => l.LearningPathId == learningPathId));

            return query;
        }

        private static IQueryable<Course> ApplyStatusFiltering(IQueryable<Course> query, int statusId)
        {
            if (statusId != 0)
            {
                var status = Enumeration.FromValue<CourseStatus>(statusId).Name;
                query = query.Where(c => c.CourseStatusName == status);
            }

            return query;
        }

        private static IQueryable<Course> ApplyOrdering(IQueryable<Course> query, GetCoursesListQuery coursesQuery)
        {
            var isSortDescending =
                Enumeration.FromValue<CourseOrderBy>(coursesQuery.OrderBy).IsSortDescending;

            var columnsMap = new Dictionary<int, Expression<Func<Course, object>>>
            {
                [new OrderByAuthor().Value] = c => c.Creator.UserName,
                [new OrderByAuthorDesc().Value] = c => c.Creator.UserName,
                [new OrderByCreationDate().Value] = c => c.CreationDate,
                [new OrderByCreationDateDesc().Value] = c => c.CreationDate
            };

            query = query.ApplyOrdering(coursesQuery, columnsMap, isSortDescending);
            return query;
        }

        private static Expression<Func<Course, CoursesListItemDto>> CourseMapping()
        {
            return course => new CoursesListItemDto
            {
                Id = course.Id,
                Title = course.Title,
                Subtitle = course.Subtitle,
                Creator = course.Creator.UserName,
                CreationDate = course.CreationDate,
                Status = course.CourseStatus.Name,
                Duration = course.Modules.SelectMany(m => m.Lectures).Sum(l => l.Duration),
                LectureCount = course.Modules.SelectMany(m => m.Lectures).Count()
            };
        }

        #endregion
    }
}
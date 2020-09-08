using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.LearningPaths.Queries;
using Solen.Core.Application.LearningPaths.Services.Queries;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Persistence.LearningPaths.Queries
{
    public class GetOtherCoursesToAddRepository : IGetOtherCoursesToAddRepository
    {
        private readonly SolenDbContext _context;

        public GetOtherCoursesToAddRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task<IList<CourseForLearningPathDto>> GetCoursesToAdd(string learningPathId, string organizationId,
            CancellationToken token)
        {
            return await _context.Courses.Where(x =>
                    x.Creator.OrganizationId == organizationId &&
                    x.CourseLearningPaths.All(c => c.LearningPathId != learningPathId))
                .OrderBy(x => x.Title)
                .Select(CourseMapping())
                .ToListAsync(token);
        }

        #region Private Methods

        private static Expression<Func<Course, CourseForLearningPathDto>> CourseMapping()
        {
            return x => new CourseForLearningPathDto
            {
                Id = x.Id,
                Title = x.Title,
                Creator = x.Creator.UserName,
                CreationDate = x.CreationDate,
                Status = x.CourseStatus.Name,
                Duration = x.Modules.SelectMany(m => m.Lectures).Sum(l => l.Duration),
            };
        }

        #endregion
    }
}
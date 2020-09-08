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
    public class GetLearningPathCoursesRepository : IGetLearningPathCoursesRepository
    {
        private readonly SolenDbContext _context;

        public GetLearningPathCoursesRepository(SolenDbContext context)
        {
            _context = context;
        }
        
        public async Task<IList<CourseForLearningPathDto>> GetLearningPathCourses(string learningPathId,
            CancellationToken token)
        {
            return await _context.LearningPathCourses
                .Where(x => x.LearningPathId == learningPathId)
                .OrderBy(x => x.Order)
                .Select(CourseMapping())
                .ToListAsync(token);
        }

        #region Private Methods

        private static Expression<Func<LearningPathCourse, CourseForLearningPathDto>> CourseMapping()
        {
            return x => new CourseForLearningPathDto
            {
                Id = x.Course.Id,
                Title = x.Course.Title,
                Creator = x.Course.Creator.UserName,
                CreationDate = x.Course.CreationDate,
                Duration = x.Course.Modules.SelectMany(m => m.Lectures).Sum(l => l.Duration),
                Status = x.Course.CourseStatus.Name,
                Order = x.Order
            };
        }

        #endregion
    }
}
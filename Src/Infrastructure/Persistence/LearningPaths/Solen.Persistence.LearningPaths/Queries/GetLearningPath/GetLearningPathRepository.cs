using System;
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
    public class GetLearningPathRepository : IGetLearningPathRepository
    {
        private readonly SolenDbContext _context;

        public GetLearningPathRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task<LearningPathDto> GetLearningPath(string learningPathId, string organizationId,
            CancellationToken token)
        {
            return await _context.LearningPaths
                .Where(x => x.Id == learningPathId && x.OrganizationId == organizationId)
                .Select(LearningPathMapping())
                .FirstOrDefaultAsync(token);
        }
        

        #region Private Methods

        private static Expression<Func<LearningPath, LearningPathDto>> LearningPathMapping()
        {
            return x => new LearningPathDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                CourseCount = x.LearningPathCourses.Count(),
                LearnerCount = x.Learners.Count,
                IsDeletable = x.IsDeletable
            };
        }

        #endregion
    }
}
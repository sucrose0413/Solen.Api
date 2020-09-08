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
    public class GetLearningPathsRepository : IGetLearningPathsRepository
    {
        private readonly SolenDbContext _context;

        public GetLearningPathsRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task<IList<LearningPathDto>> GetLearningPaths(string organizationId,
            CancellationToken token)
        {
            return await _context.LearningPaths
                .Where(x => x.OrganizationId == organizationId)
                .OrderBy(x => x.IsDeletable).ThenBy(x => x.Name)
                .Select(LearningPathsListMapping())
                .ToListAsync(token);
        }

        private Expression<Func<LearningPath, LearningPathDto>> LearningPathsListMapping()
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
    }
}
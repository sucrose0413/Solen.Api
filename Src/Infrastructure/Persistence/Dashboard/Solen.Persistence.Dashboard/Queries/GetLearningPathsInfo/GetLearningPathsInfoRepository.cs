using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.Dashboard.Queries;
using Solen.Core.Application.Dashboard.Services.Queries;

namespace Solen.Persistence.Dashboard.Queries
{
    public class GetLearningPathsInfoRepository : IGetLearningPathsInfoRepository
    {
        private readonly SolenDbContext _context;

        public GetLearningPathsInfoRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task<List<LearningPathForDashboardDto>> GetLearningPaths(string organizationId, CancellationToken token)
        {
            return await _context.LearningPaths
                .Where(x => x.OrganizationId == organizationId)
                .Select(x => new LearningPathForDashboardDto
                {
                    Name = x.Name,
                    CourseCount = x.LearningPathCourses.Count(),
                    LearnerCount = x.Learners.Count
                })
                .ToListAsync(token);
        }
    }
}
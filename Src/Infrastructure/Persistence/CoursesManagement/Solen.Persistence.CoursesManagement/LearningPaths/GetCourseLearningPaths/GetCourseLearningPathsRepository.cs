using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.CoursesManagement.Common;
using Solen.Core.Application.CoursesManagement.Services.LearningPaths;

namespace Solen.Persistence.CoursesManagement.LearningPaths
{
    public class GetCourseLearningPathsRepository : IGetCourseLearningPathsRepository
    {
        private readonly SolenDbContext _context;

        public GetCourseLearningPathsRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task<IList<string>> GetCourseLearningPathsIds(string courseId, string organizationId,
            CancellationToken token)
        {
            return await _context.LearningPathCourses
                .Where(lc => lc.CourseId == courseId && lc.Course.Creator.OrganizationId == organizationId)
                .Select(lc => lc.LearningPathId)
                .ToListAsync(token);
        }

        public async Task<IList<CourseLearningPathDto>> GetOrganizationLearningPaths(string organizationId,
            CancellationToken token)
        {
            return await _context.LearningPaths
                .Where(x => x.OrganizationId == organizationId)
                .Select(x => new CourseLearningPathDto(x.Id, x.Name))
                .ToListAsync(token);
        }
    }
}
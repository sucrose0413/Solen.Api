using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.CoursesManagement.Courses.Queries;
using Solen.Core.Application.CoursesManagement.Services.Courses;

namespace Solen.Persistence.CoursesManagement.Courses
{
    public class GetCoursesFiltersRepository : IGetCoursesFiltersRepository
    {
        private readonly SolenDbContext _context;

        public GetCoursesFiltersRepository(SolenDbContext context)
        {
            _context = context;
        }


        public async Task<IList<CoursesManagementAuthorFilterDto>> GetCoursesAuthors(string organizationId,
            CancellationToken token)
        {
            return await _context.Courses.Where(x => x.Creator.OrganizationId == organizationId)
                .Select(x => new CoursesManagementAuthorFilterDto(x.CreatorId, x.Creator.UserName))
                .Distinct()
                .ToListAsync(token);
        }

        public async Task<IList<LearningPathFilterDto>> GetLearningPaths(string organizationId, CancellationToken token)
        {
            return await _context.LearningPaths
                .Where(x => x.OrganizationId == organizationId)
                .Select(x => new LearningPathFilterDto(x.Id, x.Name))
                .ToListAsync(token);
        }
    }
}
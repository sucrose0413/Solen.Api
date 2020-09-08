using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.Learning.Queries;
using Solen.Core.Application.Learning.Services.Queries;


namespace Solen.Persistence.Learning.Queries
{
    public class GetCoursesFiltersRepository : IGetCoursesFiltersRepository
    {
        private readonly SolenDbContext _context;

        public GetCoursesFiltersRepository(SolenDbContext context)
        {
            _context = context;
        }


        public async Task<IList<LearnerCourseAuthorFilterDto>> GetCoursesAuthors(string organizationId,
            CancellationToken token)
        {
            return await _context.Courses.Where(x => x.Creator.OrganizationId == organizationId)
                .Select(x => new LearnerCourseAuthorFilterDto(x.CreatorId, x.Creator.UserName))
                .Distinct()
                .ToListAsync(token);
        }
    }
}
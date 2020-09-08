using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.CoursesManagement.EventsHandlers.Courses;
using Solen.Core.Domain.Resources.Entities;

namespace Solen.Persistence.CoursesManagement.EventsHandlers.Courses
{
    public class CourseResourcesRepo : ICourseResourcesRepo
    {
        private readonly SolenDbContext _context;

        public CourseResourcesRepo(SolenDbContext context)
        {
            _context = context;
        }
        
        public async Task<IList<AppResource>> GetCourseResources(string courseId, CancellationToken token)
        {
            return await _context.CourseResources
                .Where(x => x.CourseId == courseId)
                .Select(x => x.Resource)
                .AsNoTracking()
                .ToListAsync(token);
        }
    }
}
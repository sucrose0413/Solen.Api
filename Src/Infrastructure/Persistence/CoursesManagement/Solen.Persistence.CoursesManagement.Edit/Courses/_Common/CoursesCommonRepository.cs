using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.CoursesManagement.Edit.Services.Courses;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Persistence.CoursesManagement.Edit.Courses
{
    public class CoursesCommonRepository : ICoursesCommonRepository
    {
        private readonly SolenDbContext _context;

        public CoursesCommonRepository(SolenDbContext context)
        {
            _context = context;
        }
        
        public async Task<Course> FindCourse(string courseId, string organizationId, CancellationToken token)
        {
            return await _context.Courses
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == courseId && x.Creator.OrganizationId == organizationId, token);
        }

        public void UpdateCourse(Course course)
        {
            _context.Courses.Update(course);
        }
    }
}
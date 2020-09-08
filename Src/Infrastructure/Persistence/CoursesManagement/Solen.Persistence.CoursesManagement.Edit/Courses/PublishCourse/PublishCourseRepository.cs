using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.CoursesManagement.Edit.Services.Courses;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Persistence.CoursesManagement.Edit.Courses
{
    public class PublishCourseRepository : IPublishCourseRepository
    {
        private readonly SolenDbContext _context;

        public PublishCourseRepository(SolenDbContext context)
        {
            _context = context;
        }
        
        public async Task<Course> GetCourseWithDetails(string courseId, string organizationId, CancellationToken token)
        {
            return await _context.Courses
                .Include(c => c.CourseLearningPaths)
                .Include(c => c.Modules)
                .ThenInclude(m => m.Lectures)
                .Where(c => c.Id == courseId && c.Creator.OrganizationId == organizationId)
                .AsNoTracking()
                .SingleOrDefaultAsync(token);
        }

        public void UpdateCourse(Course course)
        {
            _context.Courses.Attach(course);
            _context.Entry(course).Property(x => x.PublicationDate).IsModified = true;
            _context.Entry(course).Property(x => x.CourseStatusName).IsModified = true;
        }
    }
}
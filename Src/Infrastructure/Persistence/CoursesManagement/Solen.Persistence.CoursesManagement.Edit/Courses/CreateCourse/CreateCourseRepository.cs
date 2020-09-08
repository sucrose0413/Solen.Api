using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.CoursesManagement.Edit.Services.Courses;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Persistence.CoursesManagement.Edit.Courses
{
    public class CreateCourseRepository : ICreateCourseRepository
    {
        private readonly SolenDbContext _context;

        public CreateCourseRepository(SolenDbContext context)
        {
            _context = context;
        }
        
        public async Task AddCourse(Course course, CancellationToken token)
        {
            await _context.Courses.AddAsync(course, token);
        }
    }
}
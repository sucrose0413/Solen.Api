using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.CoursesManagement.Edit.Services.Courses;

namespace Solen.Persistence.CoursesManagement.Edit.Courses
{
    public class UpdateCourseRepository : IUpdateCourseRepository
    {
        private readonly SolenDbContext _context;

        public UpdateCourseRepository(SolenDbContext context)
        {
            _context = context;
        }
        
        public async Task RemoveCourseSkills(string courseId, CancellationToken token)
        {
            var skills = await _context.CourseLearnedSkill
                .Where(s => s.CourseId == courseId)
                .ToListAsync(token);

            _context.CourseLearnedSkill.RemoveRange(skills);
        }
    }
}
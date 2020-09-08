using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.CoursesManagement.Edit.Services.Lectures;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Persistence.CoursesManagement.Edit.Lectures
{
    public class UploadMediaRepository : IUploadMediaRepository
    {
        private readonly SolenDbContext _context;

        public UploadMediaRepository(SolenDbContext context)
        {
            _context = context;
        }

        public void UpdateLecture(Lecture lecture)
        {
            _context.Lectures.Update(lecture);
        }

        public async Task<LectureModuleIdCourseId> GetLectureModuleIdAndCourseId(string lectureId,
            CancellationToken token)
        {
            return await _context.Lectures
                .Where(l => l.Id == lectureId)
                .Select(l => new LectureModuleIdCourseId(l.ModuleId, l.Module.CourseId))
                .SingleOrDefaultAsync(token);
        }
        public async Task AddCourseResource(CourseResource courseResource, CancellationToken token)
        {
            await _context.CourseResources.AddAsync(courseResource, token);
        }
    }
}
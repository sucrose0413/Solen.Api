using Solen.Core.Application.CoursesManagement.Edit.Services.Lectures;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Persistence.CoursesManagement.Edit.Lectures
{
    public class UpdateLectureRepository : IUpdateLectureRepository
    {
        private readonly SolenDbContext _context;

        public UpdateLectureRepository(SolenDbContext context)
        {
            _context = context;
        }
        
        public void UpdateLecture(Lecture lecture)
        {
            _context.Lectures.Update(lecture);
        }
    }
}
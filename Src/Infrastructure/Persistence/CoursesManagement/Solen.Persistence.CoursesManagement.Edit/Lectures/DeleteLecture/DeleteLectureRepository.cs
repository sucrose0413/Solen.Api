using Solen.Core.Application.CoursesManagement.Edit.Services.Lectures;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Persistence.CoursesManagement.Edit.Lectures
{
    public class DeleteLectureRepository : IDeleteLectureRepository
    {
        private readonly SolenDbContext _context;

        public DeleteLectureRepository(SolenDbContext context)
        {
            _context = context;
        }

        public void RemoveLecture(Lecture lecture)
        {
            _context.Lectures.Remove(lecture);
        }
    }
}
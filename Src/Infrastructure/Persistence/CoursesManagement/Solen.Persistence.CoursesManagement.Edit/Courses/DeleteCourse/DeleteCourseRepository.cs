using Solen.Core.Application.CoursesManagement.Edit.Services.Courses;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Persistence.CoursesManagement.Edit.Courses
{
    public class DeleteCourseRepository : IDeleteCourseRepository
    {
        private readonly SolenDbContext _context;

        public DeleteCourseRepository(SolenDbContext context)
        {
            _context = context;
        }
        
        public void RemoveCourse(Course course)
        {
            _context.Courses.Remove(course);
        }
    }
}
using Solen.Core.Application.CoursesManagement.Edit.Courses.Commands;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Services.Courses
{
    public class DeleteCourseService : IDeleteCourseService
    {
        private readonly IDeleteCourseRepository _repo;

        public DeleteCourseService(IDeleteCourseRepository repo)
        {
            _repo = repo;
        }
        
        public void RemoveCourseFromRepo(Course course)
        {
            _repo.RemoveCourse(course);
        }
    }
}
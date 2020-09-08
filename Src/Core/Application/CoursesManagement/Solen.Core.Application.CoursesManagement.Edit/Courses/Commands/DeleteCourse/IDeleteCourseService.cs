using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Courses.Commands
{
    public interface IDeleteCourseService
    {
        void RemoveCourseFromRepo(Course course);
    }
}
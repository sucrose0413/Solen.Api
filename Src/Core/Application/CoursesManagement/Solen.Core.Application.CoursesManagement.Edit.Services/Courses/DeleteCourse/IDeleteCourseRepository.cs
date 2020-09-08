using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Services.Courses
{
    public interface IDeleteCourseRepository
    {
        void RemoveCourse(Course course);
    }
}
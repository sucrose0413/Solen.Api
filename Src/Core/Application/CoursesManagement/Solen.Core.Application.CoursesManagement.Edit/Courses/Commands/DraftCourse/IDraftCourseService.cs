using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.CoursesManagement.Edit.Courses.Commands
{
    public interface IDraftCourseService
    {
        void ChangeTheCourseStatusToDraft(Course course);
    }
}
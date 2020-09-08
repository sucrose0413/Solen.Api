using Solen.Core.Application.CoursesManagement.Edit.Courses.Commands;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Courses.Enums.CourseStatuses;

namespace Solen.Core.Application.CoursesManagement.Edit.Services.Courses
{
    public class DraftCourseService : IDraftCourseService
    {
        public void ChangeTheCourseStatusToDraft(Course course)
        {
            course.ChangeCourseStatus(new DraftStatus());
        }
    }
}
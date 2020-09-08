using MediatR;

namespace Solen.Core.Application.CoursesManagement.Edit.Courses.Commands
{
    public class CourseDeletedEvent : INotification
    {
        public CourseDeletedEvent(string courseId)
        {
            CourseId = courseId;
        }

        public string CourseId { get;  }
    }
}
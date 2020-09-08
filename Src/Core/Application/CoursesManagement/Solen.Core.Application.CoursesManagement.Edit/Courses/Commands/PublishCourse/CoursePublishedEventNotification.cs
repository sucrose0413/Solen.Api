using MediatR;

namespace Solen.Core.Application.CoursesManagement.Edit.Courses.Commands
{
    public class CoursePublishedEventNotification : INotification
    {
        public CoursePublishedEventNotification(string courseId)
        {
            CourseId = courseId;
        }

        public string CourseId { get; }
    }
}
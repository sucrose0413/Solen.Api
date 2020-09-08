using Solen.Core.Application.Common.Notifications;

namespace Solen.Core.Application.CoursesManagement.EventsHandlers.Courses
{
    public class CourseInfo : NotificationData
    {
        public CourseInfo(string courseName, string creatorName)
        {
            CourseName = courseName;
            CreatorName = creatorName;
        }

        public string CourseName { get; }
        public string CreatorName { get; }
    }
}
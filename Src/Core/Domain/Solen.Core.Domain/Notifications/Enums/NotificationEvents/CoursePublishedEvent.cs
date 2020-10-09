namespace Solen.Core.Domain.Notifications.Enums.NotificationEvents
{
    public class CoursePublishedEvent : NotificationEvent
    {
        public static readonly CoursePublishedEvent Instance = new CoursePublishedEvent();
        public CoursePublishedEvent() : base(6, "CoursePublishedEvent",
            "Send a notification to the learners when an instructor publishes a course")
        {
        }
    }
}
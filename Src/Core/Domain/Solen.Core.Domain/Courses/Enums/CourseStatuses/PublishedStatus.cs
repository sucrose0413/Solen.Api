namespace Solen.Core.Domain.Courses.Enums.CourseStatuses
{
    public class PublishedStatus : CourseStatus
    {
        public static readonly PublishedStatus Instance = new PublishedStatus();
        public PublishedStatus() : base(2, "Published")
        {
        }
    }
}
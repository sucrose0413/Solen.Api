namespace Solen.Core.Domain.Courses.Enums.CourseStatuses
{
    public class UnpublishedStatus : CourseStatus
    {
        public static readonly UnpublishedStatus Instance = new UnpublishedStatus();
        private UnpublishedStatus() : base(3, "Unpublished")
        {
        }
    }
}
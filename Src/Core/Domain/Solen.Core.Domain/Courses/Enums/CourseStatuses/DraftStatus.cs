namespace Solen.Core.Domain.Courses.Enums.CourseStatuses
{
    public class DraftStatus : CourseStatus
    {
        public static readonly DraftStatus Instance = new DraftStatus();
        private DraftStatus() : base(1, "Draft")
        {
        }
    }
}
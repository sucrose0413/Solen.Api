namespace Solen.Core.Application.Dashboard.Queries
{
    public class LearnerLastCourseProgressDto
    {
        public string CourseId { get; set; }
        public string CourseTitle { get; set; }
        public int TotalDuration { get; set; }
        public int CompletedDuration { get; set; }
    }
}
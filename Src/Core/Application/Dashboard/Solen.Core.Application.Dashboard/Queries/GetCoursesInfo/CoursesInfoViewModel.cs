namespace Solen.Core.Application.Dashboard.Queries
{
    public class CoursesInfoViewModel
    {
        public LastCreatedCourseDto LastCreatedCourse { get; set; }
        public LastPublishedCourseDto LastPublishedCourse { get; set; }
        public int CourseCount { get; set; }
    }
}
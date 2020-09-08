namespace Solen.Core.Application.LearningPaths.Services.Queries
{
    public class LearningPathCourseDto
    {
        public LearningPathCourseDto(string courseId, string courseTitle, int lectureCount)
        {
            CourseId = courseId;
            CourseTitle = courseTitle;
            LectureCount = lectureCount;
        }

        public string CourseId { get; }
        public string CourseTitle { get; }
        public int LectureCount { get; }
    }
}
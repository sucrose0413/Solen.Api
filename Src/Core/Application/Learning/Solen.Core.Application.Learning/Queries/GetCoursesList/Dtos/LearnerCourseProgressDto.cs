namespace Solen.Core.Application.Learning.Queries
{
    public class LearnerCourseProgressDto
    {
        public LearnerCourseProgressDto(string courseId, int duration)
        {
            CourseId = courseId;
            Duration = duration;
        }

        public string CourseId { get;}
        public int Duration { get; }
    }
}
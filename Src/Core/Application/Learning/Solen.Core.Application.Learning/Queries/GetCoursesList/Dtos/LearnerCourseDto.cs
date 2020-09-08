using System;

namespace Solen.Core.Application.Learning.Queries
{
    public class LearnerCourseDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public string Creator { get; set; }
        public DateTime CreationDate { get; set; }
        public int Duration { get; set; }
        public int LectureCount { get; set; }
    }
}
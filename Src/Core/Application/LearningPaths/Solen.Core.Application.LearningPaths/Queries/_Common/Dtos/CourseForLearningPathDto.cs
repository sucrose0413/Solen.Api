using System;

namespace Solen.Core.Application.LearningPaths.Queries
{
    public class CourseForLearningPathDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Creator { get; set; }
        public DateTime CreationDate { get; set; }
        public string Status { get; set; }
        public int Duration { get; set; }
        public int Order { get; set; }
    }
}
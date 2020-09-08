using System;

namespace Solen.Core.Application.CoursesManagement.Courses.Queries
{
    public class CoursesListItemDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Creator { get; set; }
        public DateTime CreationDate { get; set; }
        public string Status { get; set; }
        public int Duration { get; set; }
        public int LectureCount { get; set; }
    }
}
using System;
using System.Collections.Generic;
using Solen.Core.Domain.Courses.Enums.CourseStatuses;

namespace Solen.Core.Application.CoursesManagement.Common
{
    public class CourseDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Creator { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public string Status { get; set; }
        public int Duration { get; set; }
        public int LectureCount { get; set; }
        public IList<CourseLearnedSkillDto> CourseLearnedSkills { get; set; }
        public bool IsEditable { get; set; }
        public bool IsPublished => Status == PublishedStatus.Instance.Name;
    }
}
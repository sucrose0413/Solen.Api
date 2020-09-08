using System;
using System.Collections.Generic;

namespace Solen.Core.Application.Learning.Queries
{
    public class LearnerCourseOverviewDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public string Creator { get; set; }
        public DateTime CreationDate { get; set; }
        public int Duration { get; set; }
        public int LectureCount { get; set; }

        public IEnumerable<LearnerCourseLearnedSkillDto> CourseLearnedSkills { get; set; }
        public IEnumerable<LearnerModuleOverviewDto> Modules { get; set; }
    }
}
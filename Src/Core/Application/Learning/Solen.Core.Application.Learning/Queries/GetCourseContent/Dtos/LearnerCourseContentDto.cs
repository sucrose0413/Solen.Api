using System.Collections.Generic;

namespace Solen.Core.Application.Learning.Queries
{
    public class LearnerCourseContentDto
    {
        public string CourseId { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public IList<LearnerModuleDto> Modules { get; set; }
    }
}
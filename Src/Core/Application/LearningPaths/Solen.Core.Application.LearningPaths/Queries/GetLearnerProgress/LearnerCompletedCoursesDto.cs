using System.Collections.Generic;

namespace Solen.Core.Application.LearningPaths.Queries
{
    public class LearnerCompletedCoursesDto
    {
        public IList<string> CompletedCourses { get; set; } = new List<string>();
        public int LearningPathCourseCount { get; set; }
    }
}
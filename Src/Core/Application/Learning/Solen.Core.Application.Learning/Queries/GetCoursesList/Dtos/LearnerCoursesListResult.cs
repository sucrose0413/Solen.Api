using System.Collections.Generic;

namespace Solen.Core.Application.Learning.Queries
{
    public class LearnerCoursesListResult
    {
        public LearnerCoursesListResult(int totalItems, IEnumerable<LearnerCourseDto> items,
            IEnumerable<LearnerCourseProgressDto> progress)
        {
            TotalItems = totalItems;
            Items = items ?? new List<LearnerCourseDto>();
            Progress = progress ?? new List<LearnerCourseProgressDto>();
        }

        public int TotalItems { get; }

        public IEnumerable<LearnerCourseDto> Items { get; }

        public IEnumerable<LearnerCourseProgressDto> Progress { get; }
    }
}
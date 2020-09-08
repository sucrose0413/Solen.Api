using System.Collections.Generic;

namespace Solen.Core.Application.Learning.Queries
{
    public class LearnerModuleOverviewDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public int Duration { get; set; }

        public IEnumerable<LearnerLectureOverviewDto> Lectures { get; set; }
    }
}
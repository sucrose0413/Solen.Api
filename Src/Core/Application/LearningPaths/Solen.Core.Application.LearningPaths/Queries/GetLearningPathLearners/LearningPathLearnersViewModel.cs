using System.Collections.Generic;

namespace Solen.Core.Application.LearningPaths.Queries
{
    public class LearningPathLearnersViewModel
    {
        public IEnumerable<LearnerForLearningPathDto> Learners { get; set; }
    }
}
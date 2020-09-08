using System.Collections.Generic;

namespace Solen.Core.Application.Dashboard.Queries
{
    public class LearningPathsInfoViewModel
    {
        public IEnumerable<LearningPathForDashboardDto>  LearningPaths { get; set; }
    }
}
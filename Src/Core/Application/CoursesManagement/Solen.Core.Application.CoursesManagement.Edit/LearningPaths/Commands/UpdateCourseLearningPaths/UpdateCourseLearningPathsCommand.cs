using System.Collections.Generic;
using MediatR;

namespace Solen.Core.Application.CoursesManagement.Edit.LearningPaths.Commands
{
    public class UpdateCourseLearningPathsCommand : IRequest
    {
        public string CourseId { get; set; }
        public IList<string> LearningPathsIds { get; set; }
    }
}

using System.Collections.Generic;
using MediatR;

namespace Solen.Core.Application.LearningPaths.Commands
{
    public class AddCoursesToLearningPathCommand : IRequest
    {
        public string LearningPathId { get; set; }
        public List<string> CoursesIds { get; set; }
    }
}
using Solen.Core.Application.Exceptions;

namespace Solen.Core.Application.LearningPaths.Services.Commands
{
    public class NonDeletableLearningPathException : AppBusinessException
    {
        public NonDeletableLearningPathException() : base("Non-deletable learning path")
        {
        }
    }
}
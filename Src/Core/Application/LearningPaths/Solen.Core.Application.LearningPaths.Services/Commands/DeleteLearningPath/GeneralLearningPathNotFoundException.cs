using Solen.Core.Application.Exceptions;

namespace Solen.Core.Application.LearningPaths.Services.Commands
{
    public class GeneralLearningPathNotFoundException : AppTechnicalException
    {
        public GeneralLearningPathNotFoundException() : base("General learning path not found")
        {
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.LearningPaths.Services.Commands
{
    public interface IUpdateLearningPathRepository
    {
        Task<LearningPath> GetLearningPath(string learningPathId, string organizationId, CancellationToken token);
        void UpdateLearningPath(LearningPath learningPath);
    }
}
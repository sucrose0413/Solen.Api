using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.LearningPaths.Commands
{
    public interface IUpdateLearningPathService
    {
        Task<LearningPath> GetLearningPath(string learningPathId, CancellationToken token);
        void UpdateName(LearningPath learningPath, string name);
        void UpdateDescription(LearningPath learningPath, string description);
        void UpdateLearningPathRepo(LearningPath learningPath);
    }
}
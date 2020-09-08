using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.LearningPaths.Commands
{
    public interface ICreateLearningPathService
    {
        LearningPath CreateLearningPath(string name);
        Task AddLearningPathToRepo(LearningPath learningPath, CancellationToken token);
    }
}
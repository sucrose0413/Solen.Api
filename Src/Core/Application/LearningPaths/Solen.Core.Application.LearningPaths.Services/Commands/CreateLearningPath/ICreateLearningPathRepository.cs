using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.LearningPaths.Services.Commands
{
    public interface ICreateLearningPathRepository
    {
        Task AddLearningPath(LearningPath learningPath, CancellationToken token);
    }
}
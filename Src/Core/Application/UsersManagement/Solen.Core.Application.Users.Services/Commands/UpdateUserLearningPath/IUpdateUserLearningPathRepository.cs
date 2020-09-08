using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Courses.Entities;

namespace Solen.Core.Application.Users.Services.Commands
{
    public interface IUpdateUserLearningPathRepository
    {
        Task<LearningPath> GetLearningPath(string learningPathId, CancellationToken token);
    }
}
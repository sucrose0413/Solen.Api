using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Identity.Entities;

namespace Solen.Core.Application.LearningPaths.Services.Commands
{
    public interface IDeleteLearningPathRepository
    {
        Task<LearningPath> GetLearningPath(string learningPathId, string organizationId, CancellationToken token);
        void RemoveLearningPath(LearningPath learningPath);
        Task<List<User>> GetLearningPathUsers(string learningPathId, CancellationToken token);
        Task<LearningPath> GetLearningPathByName(string learningPathName, string organizationId,
            CancellationToken token);
    }
}
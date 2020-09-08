using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Identity.Entities;

namespace Solen.Core.Application.LearningPaths.Commands
{
    public interface IDeleteLearningPathService
    {
        Task<LearningPath> GetLearningPath(string learningPathId, CancellationToken token);
        void CheckIfDeletable(LearningPath learningPath);
        Task<LearningPath> GetGeneralLearningPath(CancellationToken token);
        Task<List<User>> GetLearningPathUsers(string learningPathId, CancellationToken token);
        void ChangeUsersLearningPathToGeneral(List<User> users, LearningPath generalLearningPath);
        void UpdateUsersRepo(List<User> users);
        void RemoveLearningPathFromRepo(LearningPath learningPath);
    }
}
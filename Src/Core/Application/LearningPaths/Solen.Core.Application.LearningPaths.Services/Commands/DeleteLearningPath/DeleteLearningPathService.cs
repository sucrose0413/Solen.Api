using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Common.Identity;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.LearningPaths.Commands;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Identity.Entities;

namespace Solen.Core.Application.LearningPaths.Services.Commands
{
    public class DeleteLearningPathService : IDeleteLearningPathService
    {
        private readonly IDeleteLearningPathRepository _repo;
        private readonly IUserManager _userManager;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public DeleteLearningPathService(IDeleteLearningPathRepository repo, IUserManager userManager,
            ICurrentUserAccessor currentUserAccessor)
        {
            _repo = repo;
            _userManager = userManager;
            _currentUserAccessor = currentUserAccessor;
        }

        public async Task<LearningPath> GetLearningPath(string learningPathId, CancellationToken token)
        {
            return await _repo.GetLearningPath(learningPathId, _currentUserAccessor.OrganizationId, token) ??
                   throw new NotFoundException($"The Learning Path ({learningPathId}) does not exist");
        }

        public void CheckIfDeletable(LearningPath learningPath)
        {
            if (!learningPath.IsDeletable)
                throw new NonDeletableLearningPathException();
        }

        public async Task<List<User>> GetLearningPathUsers(string learningPathId, CancellationToken token)
        {
            return await _repo.GetLearningPathUsers(learningPathId, token);
        }

        public async Task<LearningPath> GetGeneralLearningPath(CancellationToken token)
        {
            return await _repo.GetLearningPathByName("General", _currentUserAccessor.OrganizationId, token) ??
                   throw new GeneralLearningPathNotFoundException();
        }

        public void ChangeUsersLearningPathToGeneral(List<User> users, LearningPath generalLearningPath)
        {
            users.ForEach(x => x.UpdateLearningPath(generalLearningPath));
        }

        public void UpdateUsersRepo(List<User> users)
        {
            users.ForEach(user => _userManager.UpdateUser(user));
        }

        public void RemoveLearningPathFromRepo(LearningPath learningPath)
        {
            _repo.RemoveLearningPath(learningPath);
        }
    }
}
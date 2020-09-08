using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Common.Identity;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Users.Commands;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Identity.Entities;
using Solen.Core.Domain.Identity.Enums;

namespace Solen.Core.Application.Users.Services.Commands
{
    public class InviteMembersService : IInviteMembersService
    {
        private readonly IUserManager _userManager;
        private readonly IInviteMembersRepository _repo;
        private readonly ICurrentUserAccessor _currentUserAccessor;
        private readonly IRandomTokenGenerator _tokenGenerator;

        public InviteMembersService(IUserManager userManager, IInviteMembersRepository repo,
            ICurrentUserAccessor currentUserAccessor, IRandomTokenGenerator tokenGenerator)
        {
            _userManager = userManager;
            _repo = repo;
            _currentUserAccessor = currentUserAccessor;
            _tokenGenerator = tokenGenerator;
        }

        public async Task CheckEmailExistence(string email)
        {
            if (await _userManager.DoesEmailExistAsync(email))
                throw new EmailAlreadyRegisteredException(email);
        }

        public async Task<LearningPath> GetLearningPathFromRepo(string learningPathId, CancellationToken token)
        {
            return await _repo.GetLearningPath(learningPathId, _currentUserAccessor.OrganizationId, token) ??
                   throw new NotFoundException(nameof(LearningPath), learningPathId);
        }

        public List<User> CreateUsers(IEnumerable<string> emails)
        {
            var users = new List<User>();

            foreach (var email in emails)
                users.Add(new User(email, _currentUserAccessor.OrganizationId));

            return users;
        }

        public void SetTheInviteeToUser(User user)
        {
            user.SetInvitedBy(_currentUserAccessor.Username);
        }

        public void SetTheInvitationTokenToUser(User user)
        {
            user.SetInvitationToken(_tokenGenerator.CreateToken());
        }

        public void UpdateUserLearningPath(User user, LearningPath learningPath)
        {
            user.UpdateLearningPath(learningPath);
        }

        public void AddLearnerRoleToUser(User user)
        {
            user.AddRoleId(UserRoles.Learner);
        }

        public async Task AddUserToRepo(User user)
        {
            await _userManager.CreateAsync(user);
        }
    }
}
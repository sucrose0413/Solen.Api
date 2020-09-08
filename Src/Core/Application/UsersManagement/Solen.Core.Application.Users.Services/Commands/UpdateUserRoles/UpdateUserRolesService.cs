using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Common.Identity;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Users.Commands;
using Solen.Core.Domain.Identity.Entities;
using Solen.Core.Domain.Identity.Enums;

namespace Solen.Core.Application.Users.Services.Commands
{
    public class UpdateUserRolesService : IUpdateUserRolesService
    {
        private readonly IUpdateUserRolesRepository _repo;
        private readonly IUserManager _userManager;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public UpdateUserRolesService(IUpdateUserRolesRepository repo, IUserManager userManager,
            ICurrentUserAccessor currentUserAccessor)
        {
            _repo = repo;
            _userManager = userManager;
            _currentUserAccessor = currentUserAccessor;
        }

        public void CheckIfTheUserIsTheCurrentUser(string userId)
        {
            if (userId == _currentUserAccessor.UserId)
                throw new SameUserRolesModificationException();
        }

        public async Task<User> GetUserFromRepo(string userId, CancellationToken token)
        {
            return await _userManager.FindByIdAsync(userId) ??
                   throw new NotFoundException($"The user ({userId}) not found");
        }

        public async Task CheckRole(string roleId, CancellationToken token)
        {
            if (!await _repo.DoesRoleExist(roleId, token))
                throw new NotFoundException($"The role ({roleId}) not found");
        }

        public void RemoveUserRoles(User user)
        {
            var roles = user.UserRoles.ToList();

            roles.ForEach(x => user.RemoveRoleById(x.RoleId));
        }

        public bool DoesAdminRoleIncluded(List<string> newRolesIds)
        {
            return newRolesIds.Any(x => x == UserRoles.Admin);
        }

        public void AddOnlyAdminRoleToUser(User user)
        {
            user.AddRoleId(UserRoles.Admin);
        }

        public void AddRolesToUser(User user, List<string> newRolesIds)
        {
            newRolesIds.ForEach(user.AddRoleId);
        }

        public void UpdateUserRepo(User user)
        {
            _userManager.UpdateUser(user);
        }
    }
}
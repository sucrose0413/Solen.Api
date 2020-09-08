using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Identity.Entities;

namespace Solen.Core.Application.Users.Commands
{
    public interface IUpdateUserRolesService
    {
        void CheckIfTheUserIsTheCurrentUser(string userId);
        Task<User> GetUserFromRepo(string userId, CancellationToken token);
        Task CheckRole(string roleId, CancellationToken token);
        void RemoveUserRoles(User user);
        bool DoesAdminRoleIncluded(List<string> newRolesIds);
        void AddOnlyAdminRoleToUser(User user);
        void AddRolesToUser(User user, List<string> newRolesIds);
        void UpdateUserRepo(User user);
    }
}
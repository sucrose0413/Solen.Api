using System.Collections.Generic;
using System.Threading.Tasks;
using Solen.Core.Domain.Identity.Entities;

namespace Solen.Core.Application.Common.Identity.Impl
{
    public interface IUserManagerRepo
    {
        Task AddUser(User user);
        Task<User> FindUserByIdAsync(string userId, string organizationId);
        Task<User> FindUserByEmailAsync(string email);
        Task<IList<User>> GetOrganizationUsersAsync(string organizationId);
        Task<bool> DoesEmailExistAsync(string email);
        Task<User> FindUserByInvitationTokenAsync(string invitationToken);
        Task<User> FindUserByPasswordTokenAsync(string passwordToken);
        Task<IEnumerable<Role>> GetUserRoles(string userId);
        void UpdateUser(User user);
        Task<bool> DoesPasswordTokenExist(string passwordToken);
    }
}

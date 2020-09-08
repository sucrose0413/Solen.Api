using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Solen.Core.Domain.Identity.Entities;

namespace Solen.Core.Application.Common.Identity
{
    public interface IUserManager
    {
        Task CreateAsync(User user);
        IEnumerable<Claim> CreateUserClaims(User user);
        string CreateUserToken(IEnumerable<Claim> claims);
        Task<User> FindByIdAsync(string userId);
        Task<User> FindByEmailAsync(string email);
        Task<bool> DoesEmailExistAsync(string email);
        Task<IList<User>> GetOrganizationUsersAsync();
        Task<User> FindByInvitationTokenAsync(string invitationToken);
        Task<User> FindByPasswordTokenAsync(string passwordToken);
        bool CheckPassword(User user, string password);
        bool IsActiveUser(User user);
        Task<IEnumerable<Role>> GetRolesAsync(User user);
        void ValidateUserInscription(User user);
        void UpdatePassword(User user, string password);
        void UpdateUser(User user);
        Task<bool> DoesPasswordTokenExist(string passwordToken);
    }
}
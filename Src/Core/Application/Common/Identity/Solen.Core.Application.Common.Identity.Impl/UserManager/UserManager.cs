using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Solen.Core.Application.Common.Security;
using Solen.Core.Domain.Identity.Entities;
using Solen.Core.Domain.Identity.Enums.UserStatuses;

namespace Solen.Core.Application.Common.Identity.Impl
{
    public class UserManager : IUserManager
    {
        private readonly IUserManagerRepo _repo;
        private readonly IPasswordHashGenerator _passwordHashGenerator;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public UserManager(IUserManagerRepo repo, ICurrentUserAccessor currentUserAccessor,
            IPasswordHashGenerator passwordHashGenerator, IJwtGenerator jwtGenerator)
        {
            _repo = repo;
            _currentUserAccessor = currentUserAccessor;
            _passwordHashGenerator = passwordHashGenerator;
            _jwtGenerator = jwtGenerator;
        }

        public async Task CreateAsync(User user)
        {
            await _repo.AddUser(user);
        }

        public IEnumerable<Claim> CreateUserClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName ?? ""),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("organizationId", user.OrganizationId),
                new Claim("learningPathId", user.LearningPathId ?? ""),
            };
            claims.AddRange(user.UserRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole.RoleId)));

            return claims;
        }

        public string CreateUserToken(IEnumerable<Claim> claims)
        {
            return _jwtGenerator.GenerateToken(claims);
        }

        public async Task<User> FindByIdAsync(string userId)
        {
            return await _repo.FindUserByIdAsync(userId, _currentUserAccessor.OrganizationId);
        }

        public void ValidateUserInscription(User user)
        {
            user.ChangeUserStatus(new ActiveStatus());
            user.InitInvitationToken();
        }

        public void UpdatePassword(User user, string password)
        {
            _passwordHashGenerator.CreatePasswordHash(password, out var passwordHash, out var passwordSalt);
            user.UpdatePassword(passwordHash, passwordSalt);
        }

        public void UpdateUser(User user)
        {
            _repo.UpdateUser(user);
        }

        public async Task<bool> DoesPasswordTokenExist(string passwordToken)
        {
            return await _repo.DoesPasswordTokenExist(passwordToken);
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _repo.FindUserByEmailAsync(email);
        }

        public async Task<bool> DoesEmailExistAsync(string email)
        {
            return await _repo.DoesEmailExistAsync(email);
        }

        public async Task<IList<User>> GetOrganizationUsersAsync()
        {
            return await _repo.GetOrganizationUsersAsync(_currentUserAccessor.OrganizationId);
        }

        public async Task<User> FindByInvitationTokenAsync(string invitationToken)
        {
            return await _repo.FindUserByInvitationTokenAsync(invitationToken);
        }

        public async Task<User> FindByPasswordTokenAsync(string passwordToken)
        {
            return await _repo.FindUserByPasswordTokenAsync(passwordToken);
        }

        public bool CheckPassword(User user, string password)
        {
            return user != null &&
                   _passwordHashGenerator.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt);
        }

        public bool IsActiveUser(User user)
        {
            return user != null && user.UserStatus.Name == new ActiveStatus().Name;
        }


        public async Task<IEnumerable<Role>> GetRolesAsync(User user)
        {
            return await _repo.GetUserRoles(user.Id);
        }
    }
}
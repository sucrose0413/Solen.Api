namespace Solen.Core.Domain.Identity.Entities
{
    public class UserRole
    {
        private UserRole()
        {
        }

        public UserRole(string userId, string roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }

        public UserRole(string userId, Role role)
        {
            UserId = userId;
            RoleId = role.Id;
            Role = role;
        }

        public string UserId { get; private set; }
        public User User { get; private set; }
        public string RoleId { get; private set; }
        public Role Role { get; private set; }
    }
}
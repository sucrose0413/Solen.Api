using System.Collections.Generic;

namespace Solen.Core.Application.Users.Queries
{
    public class UserViewModel
    {
        public UserDto User { get; set; }
        public IEnumerable<LearningPathForUserDto> LearningPaths { get; set; }
        public IEnumerable<RoleForUserDto> Roles { get; set; }
    }
}
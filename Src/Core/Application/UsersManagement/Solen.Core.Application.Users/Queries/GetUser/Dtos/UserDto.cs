using System;
using System.Collections.Generic;

namespace Solen.Core.Application.Users.Queries
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string LearningPathId { get; set; }
        public IEnumerable<string> RolesIds { get; set; }
        public string Status { get; set; }
        public bool IsBlocked { get; set; }
        public string InvitedBy { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
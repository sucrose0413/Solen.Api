using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Common.Identity;
using Solen.Core.Application.Users.Queries;
using Solen.Core.Domain.Identity.Entities;

namespace Solen.Core.Application.Users.Services.Queries
{
    public class GetUsersListService : IGetUsersListService
    {
        private readonly IUserManager _userManager;

        public GetUsersListService(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<IList<UsersListItemDto>> GetUsersList(CancellationToken token)
        {
            var users = await _userManager.GetOrganizationUsersAsync();

            return users.Select(UsersListItemDtoMapping())
                .OrderBy(x => x.Status).ThenBy(x => x.UserName)
                .ToList();
        }

        private static Func<User, UsersListItemDto> UsersListItemDtoMapping()
        {
            return u => new UsersListItemDto
            {
                Id = u.Id,
                Email = u.Email,
                UserName = u.UserName,
                Roles = string.Join(", ", u.UserRoles.Select(ur => ur.Role.Name)),
                Status = u.UserStatusName,
                LearningPath = u.LearningPath?.Name
            };
        }
    }
}
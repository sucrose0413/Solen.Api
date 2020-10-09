using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Common.Identity;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Users.Queries;
using Solen.Core.Domain.Identity.Enums.UserStatuses;

namespace Solen.Core.Application.Users.Services.Queries
{
    public class GetUserService : IGetUserService
    {
        private readonly IGetUserRepository _repo;
        private readonly IUserManager _userManager;
        private readonly IRoleManager _roleManager;
        private readonly ICurrentUserAccessor _currentUserAccessor;


        public GetUserService(IGetUserRepository repo, IUserManager userManager, IRoleManager roleManager,
            ICurrentUserAccessor currentUserAccessor)
        {
            _repo = repo;
            _userManager = userManager;
            _roleManager = roleManager;
            _currentUserAccessor = currentUserAccessor;
        }

        public async Task<UserDto> GetUser(string userId, CancellationToken token)
        {
            var user = await _userManager.FindByIdAsync(userId) ??
                       throw new NotFoundException($"The User ({userId}) does not exist");

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                CreationDate = user.CreationDate,
                InvitedBy = user.InvitedBy,
                LearningPathId = user.LearningPath?.Id,
                RolesIds = user.UserRoles.Select(ur => ur.RoleId),
                Status = user.UserStatusName,
                IsBlocked = user.UserStatusName == BlockedStatus.Instance.Name,
                UserName = user.UserName
            };
        }

        public async Task<IList<LearningPathForUserDto>> GetLearningPaths(CancellationToken token)
        {
            return await _repo.GetLearningPaths(_currentUserAccessor.OrganizationId, token);
        }

        public async Task<IList<RoleForUserDto>> GetRoles(CancellationToken token)
        {
            var roles = await _roleManager.GetRoles();

            return roles.Select(x => new RoleForUserDto(x.Id, x.Name, x.Description)).ToList();
        }
    }
}
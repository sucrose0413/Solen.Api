using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Solen.Core.Application.Users.Queries
{
    public interface IGetUserService
    {
        Task<UserDto> GetUser(string userId, CancellationToken token);
        Task<IList<LearningPathForUserDto>> GetLearningPaths(CancellationToken token);
        Task<IList<RoleForUserDto>> GetRoles(CancellationToken token);
    }
}
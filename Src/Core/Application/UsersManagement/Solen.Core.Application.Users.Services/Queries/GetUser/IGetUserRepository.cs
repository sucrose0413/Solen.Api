using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Users.Queries;

namespace Solen.Core.Application.Users.Services.Queries
{
    public interface IGetUserRepository
    {
        Task<IList<LearningPathForUserDto>> GetLearningPaths(string organizationId, CancellationToken token);
    }
}
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Solen.Core.Application.Users.Queries
{
    public interface IGetUsersListService
    {
        Task<IList<UsersListItemDto>> GetUsersList(CancellationToken token);
    }
}
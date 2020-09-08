using System.Threading;
using System.Threading.Tasks;

namespace Solen.Core.Application.Users.Services.Commands
{
    public interface IUpdateUserRolesRepository
    {
        Task<bool> DoesRoleExist(string roleId, CancellationToken token);
    }
}
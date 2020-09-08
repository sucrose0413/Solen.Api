using System.Collections.Generic;
using System.Threading.Tasks;
using Solen.Core.Domain.Identity.Entities;

namespace Solen.Core.Application.Common.Identity
{
    public interface IRoleManager
    {
        Task<IList<Role>> GetRoles();
        Task<bool> DoesRoleExist(string roleId);
    }
}
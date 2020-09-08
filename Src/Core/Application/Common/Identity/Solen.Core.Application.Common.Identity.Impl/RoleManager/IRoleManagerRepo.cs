using System.Collections.Generic;
using System.Threading.Tasks;
using Solen.Core.Domain.Identity.Entities;

namespace Solen.Core.Application.Common.Identity.Impl
{
    public interface IRoleManagerRepo
    {
        Task<IList<Role>> GetRoles();
        Task<bool> DoesRoleExist(string roleId);
    }
}
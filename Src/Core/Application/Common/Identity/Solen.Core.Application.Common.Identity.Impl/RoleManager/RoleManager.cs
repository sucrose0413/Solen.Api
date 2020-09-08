using System.Collections.Generic;
using System.Threading.Tasks;
using Solen.Core.Domain.Identity.Entities;

namespace Solen.Core.Application.Common.Identity.Impl
{
    public class RoleManager : IRoleManager
    {
        private readonly IRoleManagerRepo _repo;

        public RoleManager(IRoleManagerRepo repo)
        {
            _repo = repo;
        }

        public async Task<IList<Role>> GetRoles()
        {
            return await _repo.GetRoles();
        }

        public async Task<bool> DoesRoleExist(string roleId)
        {
            return await _repo.DoesRoleExist(roleId);
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.Common.Identity.Impl;
using Solen.Core.Domain.Identity.Entities;

namespace Solen.Persistence.Common.Identity
{
    public class RoleManagerRepo : IRoleManagerRepo
    {
        private readonly SolenDbContext _context;

        public RoleManagerRepo(SolenDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Role>> GetRoles()
        {
            return await _context.Roles
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> DoesRoleExist(string roleId)
        {
            return await _context.Roles.AnyAsync(x => x.Id == roleId);
        }
    }
}

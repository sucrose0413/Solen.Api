using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.Users.Services.Commands;

namespace Solen.Persistence.Users.Commands
{
    public class UpdateUserRolesRepository : IUpdateUserRolesRepository
    {
        private readonly SolenDbContext _context;

        public UpdateUserRolesRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DoesRoleExist(string roleId, CancellationToken token)
        {
            return await _context.Roles.AnyAsync(x => x.Id == roleId, token);
        }
    }
}
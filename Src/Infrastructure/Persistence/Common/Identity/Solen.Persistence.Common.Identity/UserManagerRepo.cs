using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.Common.Identity.Impl;
using Solen.Core.Domain.Identity.Entities;

namespace Solen.Persistence.Common.Identity
{
    public class UserManagerRepo : IUserManagerRepo
    {
        private readonly SolenDbContext _context;

        public UserManagerRepo(SolenDbContext context)
        {
            _context = context;
        }

        public async Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<User> FindUserByIdAsync(string userId, string organizationId)
        {
            return await _context.Users
                .Include(u => u.Organization)
                .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                .Include(u => u.LearningPath)
                .FirstOrDefaultAsync(x => x.Id == userId && x.OrganizationId == organizationId);
        }

        public async Task<User> FindUserByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.Organization)
                .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                .Include(u => u.LearningPath)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == email.ToLower());
        }

        public async Task<IList<User>> GetOrganizationUsersAsync(string organizationId)
        {
            return await _context.Users
                .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                .Include(u => u.LearningPath)
                .Where(x => x.OrganizationId == organizationId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> DoesEmailExistAsync(string email)
        {
            return await _context.Users.AnyAsync(x => x.Email == email);
        }

        public async Task<User> FindUserByInvitationTokenAsync(string invitationToken)
        {
            return await _context.Users
                .Include(u => u.Organization)
                .Include(u => u.UserRoles)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.InvitationToken == invitationToken);
        }

        public async Task<User> FindUserByPasswordTokenAsync(string passwordToken)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.PasswordToken == passwordToken);
        }

        public async Task<IEnumerable<Role>> GetUserRoles(string userId)
        {
            return await _context.UserRoles
                .Include(ur => ur.Role)
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.Role)
                .AsNoTracking()
                .ToListAsync();
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
        }

        public async Task<bool> DoesPasswordTokenExist(string passwordToken)
        {
            return await _context.Users.AnyAsync(x => x.PasswordToken == passwordToken);
        }
    }
}
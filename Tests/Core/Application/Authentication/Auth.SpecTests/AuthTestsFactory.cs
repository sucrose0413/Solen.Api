using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen;
using Solen.Core.Domain.Identity.Entities;
using Solen.Core.Domain.Security.Entities;
using Solen.SpecTests;

namespace Auth.SpecTests
{
    public class AuthTestsFactory : BaseWebApplicationFactory<Startup>
    {
        public void AddRefreshToken(RefreshToken refreshToken)
        {
            Context.RefreshTokens.Add(refreshToken);
            Context.SaveChanges();
        }
        
        public async Task<User> GetUserById(string userId)
        {
            return await Context.Users.AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == userId);
        }
    }
}
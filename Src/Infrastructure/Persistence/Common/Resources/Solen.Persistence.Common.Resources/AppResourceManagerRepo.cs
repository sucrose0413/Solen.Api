using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.Common.Resources.Impl;
using Solen.Core.Domain.Resources.Entities;

namespace Solen.Persistence.Common.Resources
{
    public class AppResourceManagerRepo : IAppResourceManagerRepo
    {
        private readonly SolenDbContext _context;

        public AppResourceManagerRepo(SolenDbContext context)
        {
            _context = context;
        }

        public void AddResource(AppResource appResource)
        {
            _context.AppResources.AddAsync(appResource);
        }

        public Task UpdateResource(AppResource appResource)
        {
            _context.AppResources.Update(appResource);

            return Task.CompletedTask;
        }

        public void RemoveResource(AppResource appResource)
        {
            _context.AppResources.Remove(appResource);
        }

        public async Task<IList<AppResource>> GetResourcesToDelete(CancellationToken token)
        {
            return await _context.AppResources.Where(x => x.ToDelete)
                .AsNoTracking()
                .ToListAsync(token);
        }
    }
}
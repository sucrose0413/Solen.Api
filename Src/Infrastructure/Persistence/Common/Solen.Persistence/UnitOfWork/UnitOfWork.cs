using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.UnitOfWork;

namespace Solen.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SolenDbContext _context;

        public UnitOfWork(SolenDbContext context)
        {
            _context = context;
        }
        public async Task<int> SaveAsync(CancellationToken cancellationToken)
        {
           return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

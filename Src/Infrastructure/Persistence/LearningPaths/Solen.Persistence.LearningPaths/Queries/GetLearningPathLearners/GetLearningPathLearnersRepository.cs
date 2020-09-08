using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.LearningPaths.Queries;
using Solen.Core.Application.LearningPaths.Services.Queries;
using Solen.Core.Domain.Identity.Entities;

namespace Solen.Persistence.LearningPaths.Queries
{
    public class GetLearningPathLearnersRepository : IGetLearningPathLearnersRepository
    {
        private readonly SolenDbContext _context;

        public GetLearningPathLearnersRepository(SolenDbContext context)
        {
            _context = context;
        }
        
        public async Task<IList<LearnerForLearningPathDto>> GetLearningPathLearners(string learningPathId,
            CancellationToken token)
        {
            return await _context.Users.Where(x => x.LearningPathId == learningPathId)
                .OrderBy(x => x.UserName)
                .Select(LearnerMapping())
                .ToListAsync(token);
        }

        #region Private methods

        private static Expression<Func<User, LearnerForLearningPathDto>> LearnerMapping()
        {
            return x => new LearnerForLearningPathDto
            {
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
                Status = x.UserStatusName
            };
        }

        #endregion
    }
}
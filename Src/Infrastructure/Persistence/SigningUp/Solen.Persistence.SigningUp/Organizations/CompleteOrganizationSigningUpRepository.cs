using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solen.Core.Application.SigningUp.Services.Organizations;
using Solen.Core.Domain.Common.Entities;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Subscription.Entities;

namespace Solen.Persistence.SigningUp.Organizations
{
    public class CompleteOrganizationSigningUpRepository : ICompleteOrganizationSigningUpRepository
    {
        private readonly SolenDbContext _context;

        public CompleteOrganizationSigningUpRepository(SolenDbContext context)
        {
            _context = context;
        }

        public async Task AddOrganization(Organization organization,
            CancellationToken token)
        {
            await _context.Organizations.AddAsync(organization, token);
        }

        public async Task<OrganizationSigningUp> GetSigningUpByToken(string signingUpToken, CancellationToken token)
        {
            return await _context.OrganizationSigningUps
                .Where(x => x.Token == signingUpToken)
                .AsNoTracking()
                .FirstOrDefaultAsync(token);
        }

        public void RemoveSigningUp(OrganizationSigningUp signingUp)
        {
            _context.OrganizationSigningUps.Remove(signingUp);
        }

        public void AddLearningPath(LearningPath learningPath)
        {
            _context.LearningPaths.Add(learningPath);
        }
    }
}
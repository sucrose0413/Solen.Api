using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Common.Entities;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Subscription.Entities;

namespace Solen.Core.Application.SigningUp.Services.Organizations
{
    public interface ICompleteOrganizationSigningUpRepository
    {
        Task AddOrganization(Organization organization, CancellationToken token);
        Task<OrganizationSigningUp> GetSigningUpByToken(string signingUpToken, CancellationToken token);
        void RemoveSigningUp(OrganizationSigningUp signingUp);
        void AddLearningPath(LearningPath learningPath);
    }
}
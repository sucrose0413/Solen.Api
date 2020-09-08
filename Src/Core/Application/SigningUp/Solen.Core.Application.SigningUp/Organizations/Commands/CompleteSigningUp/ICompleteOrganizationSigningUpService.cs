using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Domain.Common.Entities;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Identity.Entities;
using Solen.Core.Domain.Subscription.Entities;

namespace Solen.Core.Application.SigningUp.Organizations.Commands
{
    public interface ICompleteOrganizationSigningUpService
    {
        Task<OrganizationSigningUp> GetSigningUp(string signingUpToken, CancellationToken token);
        Organization CreateOrganization(string organizationName);
        Task AddOrganizationToRepo(Organization organization, CancellationToken token);
        User CreateAdminUser(string email, string userName, string organizationId);
        void ValidateAdminUserInscription(User adminUser, string password);
        Task AddAdminUserToRepo(User adminUser, CancellationToken token);
        void RemoveSigninUpFromRepo(OrganizationSigningUp signingUp);
        LearningPath CreateGeneralLearningPath(string organizationId);
        void AddGeneralLearningPathToRepo(LearningPath generalLearningPath);
        void UpdateAdminUserLearningPath(User user, LearningPath learningPath);
    }
}
using System.Threading;
using System.Threading.Tasks;
using Solen.Core.Application.Common.Identity;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.SigningUp.Organizations.Commands;
using Solen.Core.Domain.Common.Entities;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Identity.Entities;
using Solen.Core.Domain.Identity.Enums;
using Solen.Core.Domain.Subscription.Constants;
using Solen.Core.Domain.Subscription.Entities;

namespace Solen.Core.Application.SigningUp.Services.Organizations
{
    public class CompleteOrganizationSigningUpService : ICompleteOrganizationSigningUpService
    {
        private readonly ICompleteOrganizationSigningUpRepository _repo;
        private readonly IUserManager _userManager;

        public CompleteOrganizationSigningUpService(ICompleteOrganizationSigningUpRepository repo,
            IUserManager userManager)
        {
            _repo = repo;
            _userManager = userManager;
        }

        public async Task<OrganizationSigningUp> GetSigningUp(string signingUpToken, CancellationToken token)
        {
            return await _repo.GetSigningUpByToken(signingUpToken, token) ??
                   throw new NotFoundException("token invalid or not found");
        }

        public Organization CreateOrganization(string organizationName)
        {
            return new Organization(organizationName, SubscriptionPlans.Free);
        }

        public async Task AddOrganizationToRepo(Organization organization, CancellationToken token)
        {
            await _repo.AddOrganization(organization, token);
        }

        public User CreateAdminUser(string email, string userName, string organizationId)
        {
            var user = new User(email, organizationId);
            user.UpdateUserName(userName);
            user.AddRoleId(UserRoles.Admin);
            return user;
        }

        public void ValidateAdminUserInscription(User adminUser, string password)
        {
            _userManager.UpdatePassword(adminUser, password);
            _userManager.ValidateUserInscription(adminUser);
        }

        public async Task AddAdminUserToRepo(User adminUser, CancellationToken token)
        {
            await _userManager.CreateAsync(adminUser);
        }

        public void RemoveSigninUpFromRepo(OrganizationSigningUp signingUp)
        {
            _repo.RemoveSigningUp(signingUp);
        }

        public LearningPath CreateGeneralLearningPath(string organizationId)
        {
            var generalLearningPath = new LearningPath("General", organizationId, isDeletable: false);
            generalLearningPath.UpdateDescription("The user default learning path when the user account is created");

            return generalLearningPath;
        }

        public void AddGeneralLearningPathToRepo(LearningPath generalLearningPath)
        {
            _repo.AddLearningPath(generalLearningPath);
        }

        public void UpdateAdminUserLearningPath(User user, LearningPath learningPath)
        {
            user.UpdateLearningPath(learningPath);
        }
    }
}
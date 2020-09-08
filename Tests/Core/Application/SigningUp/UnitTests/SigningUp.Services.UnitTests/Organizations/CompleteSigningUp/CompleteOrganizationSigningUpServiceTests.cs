using System.Linq;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Identity;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.SigningUp.Services.Organizations;
using Solen.Core.Domain.Common.Entities;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Identity.Entities;
using Solen.Core.Domain.Identity.Enums;
using Solen.Core.Domain.Subscription.Constants;
using Solen.Core.Domain.Subscription.Entities;

namespace SigningUp.Services.UnitTests.Organizations.CompleteSigningUp
{
    [TestFixture]
    public class CompleteOrganizationSigningUpServiceTests
    {
        private Mock<ICompleteOrganizationSigningUpRepository> _repo;
        private Mock<IUserManager> _userManager;
        private CompleteOrganizationSigningUpService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<ICompleteOrganizationSigningUpRepository>();
            _userManager = new Mock<IUserManager>();
            _sut = new CompleteOrganizationSigningUpService(_repo.Object, _userManager.Object);
        }

        [Test]
        public void GetSigningUp_SigninUpTokenIsNotFoundOrInvalid_ThrowNotFoundException()
        {
            _repo.Setup(x => x.GetSigningUpByToken("token", default))
                .ReturnsAsync((OrganizationSigningUp) null);

            Assert.That(() => _sut.GetSigningUp("token", default),
                Throws.Exception.TypeOf<NotFoundException>());
        }

        [Test]
        public void GetSigningUp_SigninUpTokenIsValid_ReturnOrganizationSigningUp()
        {
            var signingUp = new OrganizationSigningUp("email", "token");
            _repo.Setup(x => x.GetSigningUpByToken("token", default))
                .ReturnsAsync(signingUp);

            var result = _sut.GetSigningUp("token", default).Result;

            Assert.That(result, Is.EqualTo(signingUp));
        }

        [Test]
        public void CreateOrganization_WhenCalled_CreateOrganization()
        {
            var result = _sut.CreateOrganization("organization name");

            Assert.That(result.Name, Is.EqualTo("organization name"));
            Assert.That(result.SubscriptionPlanId, Is.EqualTo(SubscriptionPlans.Free));
        }

        [Test]
        public void AddOrganizationToRepo_WhenCalled_AddTheOrganizationToRepo()
        {
            var organization = new Organization("organization name", SubscriptionPlans.Free);

            _sut.AddOrganizationToRepo(organization, default).Wait();

            _repo.Verify(x => x.AddOrganization(organization, default));
        }

        [Test]
        public void CreateAdminUser_WhenCalled_CreateAdminUser()
        {
            var adminUser = _sut.CreateAdminUser("email", "userName", "organizationId");

            Assert.That(adminUser.Email, Is.EqualTo("email"));
            Assert.That(adminUser.UserName, Is.EqualTo("userName"));
            Assert.That(adminUser.OrganizationId, Is.EqualTo("organizationId"));
            Assert.That(adminUser.UserRoles.Count(x => x.RoleId == UserRoles.Admin), Is.EqualTo(1));
        }

        [Test]
        public void ValidateAdminUserInscription_WhenCalled_ValidateAdminUserInscription()
        {
            var adminUser = new User("email", "organizationId");
            var password = "password";

            _sut.ValidateAdminUserInscription(adminUser, password);

            _userManager.Verify(x => x.UpdatePassword(adminUser, password));
            _userManager.Verify(x => x.ValidateUserInscription(adminUser));
        }

        [Test]
        public void AddAdminUserToRepo_WhenCalled_AddAdminUserToRepo()
        {
            var adminUser = new User("email", "organizationId");

            _sut.AddAdminUserToRepo(adminUser, default).Wait();

            _userManager.Verify(x => x.CreateAsync(adminUser));
        }

        [Test]
        public void RemoveSigninUpFromRepo_WhenCalled_RemoveSigninUpFromRepo()
        {
            var signingUp = new OrganizationSigningUp("email", "token");

            _sut.RemoveSigninUpFromRepo(signingUp);

            _repo.Verify(x => x.RemoveSigningUp(signingUp));
        }

        [Test]
        public void CreateGeneralLearningPath_WhenCalled_CreateGeneralLearningPath()
        {
            var result = _sut.CreateGeneralLearningPath("organizationId");

            Assert.That(result.Name, Is.EqualTo("General"));
            Assert.That(result.IsDeletable, Is.False);
        }

        [Test]
        public void AddGeneralLearningPathToRepo_WhenCalled_AddGeneralLearningPathToRepo()
        {
            var generalLearningPath = new LearningPath("General", "organizationId");

            _sut.AddGeneralLearningPathToRepo(generalLearningPath);

            _repo.Verify(x => x.AddLearningPath(generalLearningPath));
        }
        
        [Test]
        public void UpdateAdminUserLearningPath_WhenCalled_UpdateAdminUserLearningPath()
        {
            var adminUser = new Mock<User>("email", "organizationId");
            var generalLearningPath = new LearningPath("General", "organizationId");

            _sut.UpdateAdminUserLearningPath(adminUser.Object, generalLearningPath);

            adminUser.Verify(x => x.UpdateLearningPath(generalLearningPath));
        }
    }
}
using System.Linq;
using System.Security.Claims;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Identity.Impl;
using Solen.Core.Application.Common.Security;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Identity.Entities;

namespace Common.Identity.UnitTests
{
    [TestFixture]
    public class CreateUserClaimsTests
    {
        private UserManager _sut;
        private Mock<IUserManagerRepo> _repo;
        private Mock<IPasswordHashGenerator> _passwordHashGenerator;
        private Mock<IJwtGenerator> _jwtGenerator;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IUserManagerRepo>();
            _passwordHashGenerator = new Mock<IPasswordHashGenerator>();
            _jwtGenerator = new Mock<IJwtGenerator>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();

            _sut = new UserManager(_repo.Object, _currentUserAccessor.Object, _passwordHashGenerator.Object,
                _jwtGenerator.Object);

            _currentUserAccessor.Setup(x => x.OrganizationId).Returns("organizationId");
        }

        [Test]
        public void WhenCalled_CreateTheCorrectNameIdentifierClaim()
        {
            var user = new User("email", "organizationId");

            var result = _sut.CreateUserClaims(user);

            var nameIdentifierValue = result.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            Assert.That(nameIdentifierValue, Is.EqualTo(user.Id));
        }

        [Test]
        public void WhenCalled_CreateTheCorrectNameClaim()
        {
            var user = new User("email", "organizationId");
            user.UpdateUserName("user name");

            var result = _sut.CreateUserClaims(user);

            var nameValue = result.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            Assert.That(nameValue, Is.EqualTo(user.UserName));
        }

        [Test]
        public void WhenCalled_CreateTheCorrectEmailClaim()
        {
            var user = new User("email", "organizationId");

            var result = _sut.CreateUserClaims(user);

            var emailValue = result.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            Assert.That(emailValue, Is.EqualTo(user.Email));
        }

        [Test]
        public void WhenCalled_CreateTheCorrectOrganizationIdClaim()
        {
            var user = new User("email", "organizationId");

            var result = _sut.CreateUserClaims(user);

            var organizationValue = result.FirstOrDefault(x => x.Type == "organizationId")?.Value;
            Assert.That(organizationValue, Is.EqualTo(user.OrganizationId));
        }

        [Test]
        public void WhenCalled_CreateTheCorrectLearningPathIdClaim()
        {
            var user = new User("email", "organizationId");
            var learningPath = new LearningPath("name", "organizationId");
            user.UpdateLearningPath(learningPath);

            var result = _sut.CreateUserClaims(user);

            var learningPathValue = result.FirstOrDefault(x => x.Type == "learningPathId")?.Value;
            Assert.That(learningPathValue, Is.EqualTo(user.LearningPath.Id));
        }

        [Test]
        public void WhenCalled_CreateTheCorrectRolesClaims()
        {
            var user = new User("email", "organizationId");
            user.AddRoleId("roleId1");
            user.AddRoleId("roleId2");

            var result = _sut.CreateUserClaims(user).ToList();

            var rolesClaims = result.Where(x => x.Type == ClaimTypes.Role).ToList();
            Assert.That(rolesClaims.Count(x => x.Value == "roleId1"), Is.EqualTo(1));
            Assert.That(rolesClaims.Count(x => x.Value == "roleId2"), Is.EqualTo(1));
        }
    }
}
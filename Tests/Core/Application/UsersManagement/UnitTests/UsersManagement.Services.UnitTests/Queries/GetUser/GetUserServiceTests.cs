using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Identity;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Users.Queries;
using Solen.Core.Application.Users.Services.Queries;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Identity.Entities;
using Solen.Core.Domain.Identity.Enums.UserStatuses;

namespace UsersManagement.Services.UnitTests.Queries.GetUser
{
    [TestFixture]
    public class GetUserServiceTests
    {
        private Mock<IGetUserRepository> _repo;
        private Mock<IUserManager> _userManager;
        private Mock<IRoleManager> _roleManager;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;
        private GetUserService _sut;

        private User _userToReturn;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IGetUserRepository>();
            _userManager = new Mock<IUserManager>();
            _roleManager = new Mock<IRoleManager>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();
            _sut = new GetUserService(_repo.Object, _userManager.Object, _roleManager.Object,
                _currentUserAccessor.Object);

            _currentUserAccessor.Setup(x => x.OrganizationId).Returns("organizationId");

            _userToReturn = new User("email", "organizationId");
            _userManager.Setup(x => x.FindByIdAsync("userId")).ReturnsAsync(_userToReturn);
        }

        [Test]
        public void GetUser_TheUserDoesNotExist_ThrowNotFoundException()
        {
            _userManager.Setup(x => x.FindByIdAsync("userId")).ReturnsAsync((User) null);

            Assert.That(() => _sut.GetUser("userId", default),
                Throws.TypeOf<NotFoundException>());
        }

        [Test]
        public void GetUser_TheUserDoesExist_ReturnUserId()
        {
            var result = _sut.GetUser("userId", default).Result;

            Assert.That(result.Id, Is.EqualTo(_userToReturn.Id));
        }

        [Test]
        public void GetUser_TheUserDoesExist_ReturnUserEmail()
        {
            var result = _sut.GetUser("userId", default).Result;

            Assert.That(result.Email, Is.EqualTo(_userToReturn.Email));
        }

        [Test]
        public void GetUser_TheUserDoesExist_ReturnUserCreationDate()
        {
            var result = _sut.GetUser("userId", default).Result;

            Assert.That(result.CreationDate, Is.EqualTo(_userToReturn.CreationDate));
        }

        [Test]
        public void GetUser_TheUserDoesExist_ReturnUserInvitedBy()
        {
            _userToReturn.SetInvitedBy("invitedBy");

            var result = _sut.GetUser("userId", default).Result;

            Assert.That(result.InvitedBy, Is.EqualTo(_userToReturn.InvitedBy));
        }

        [Test]
        public void GetUser_TheUserDoesExist_ReturnLearningPathId()
        {
            var learningPath = new LearningPath("name", "organizationId");
            _userToReturn.UpdateLearningPath(learningPath);

            var result = _sut.GetUser("userId", default).Result;

            Assert.That(result.LearningPathId, Is.EqualTo(_userToReturn.LearningPathId));
        }

        [Test]
        public void GetUser_TheUserDoesExist_ReturnRolesIds()
        {
            _userToReturn.AddRoleId("role1");
            _userToReturn.AddRoleId("role2");

            var result = _sut.GetUser("userId", default).Result;

            Assert.That(result.RolesIds, Is.EqualTo(new[] {"role1", "role2"}));
        }

        [Test]
        public void GetUser_TheUserDoesExist_ReturnUserStatus()
        {
            var result = _sut.GetUser("userId", default).Result;

            Assert.That(result.Status, Is.EqualTo(_userToReturn.UserStatusName));
        }

        [Test]
        public void GetUser_TheUserDoesExistAndBlocked_IsBlockedShouldBeTrue()
        {
            _userToReturn.ChangeUserStatus(BlockedStatus.Instance);

            var result = _sut.GetUser("userId", default).Result;

            Assert.That(result.IsBlocked, Is.True);
        }

        [Test]
        public void GetUser_TheUserDoesExistAndActive_IsBlockedShouldBeFalse()
        {
            _userToReturn.ChangeUserStatus(ActiveStatus.Instance);

            var result = _sut.GetUser("userId", default).Result;

            Assert.That(result.IsBlocked, Is.False);
        }

        [Test]
        public void GetUser_TheUserDoesExist_ReturnUserName()
        {
            _userToReturn.UpdateUserName("user name");

            var result = _sut.GetUser("userId", default).Result;

            Assert.That(result.UserName, Is.EqualTo(_userToReturn.UserName));
        }

        [Test]
        public void GetLearningPaths_WhenCalled_ReturnLearningPathsList()
        {
            var learningPaths = new List<LearningPathForUserDto>();
            _repo.Setup(x => x.GetLearningPaths("organizationId", default))
                .ReturnsAsync(learningPaths);

            var result = _sut.GetLearningPaths(default).Result;

            Assert.That(result, Is.EqualTo(learningPaths));
        }

        [Test]
        public void GetRoles_WhenCalled_ReturnRolesList()
        {
            var roles = new List<Role> {new Role("roleId", "roleName", "roleDescription")};
            _roleManager.Setup(x => x.GetRoles())
                .ReturnsAsync(roles);

            var result = _sut.GetRoles(default).Result;

            Assert.That(
                result.Count(x => x.Id == "roleId" && x.Name == "roleName" && x.Description == "roleDescription"),
                Is.EqualTo(1));
        }
    }
}
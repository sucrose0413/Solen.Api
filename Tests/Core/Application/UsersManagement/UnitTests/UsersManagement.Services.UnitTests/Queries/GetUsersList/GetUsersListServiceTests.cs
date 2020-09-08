using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Identity;
using Solen.Core.Application.Users.Services.Queries;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Identity.Entities;

namespace UsersManagement.Services.UnitTests.Queries.GetUsersList
{
    [TestFixture]
    public class GetUsersListServiceTests
    {
        private Mock<IUserManager> _userManager;
        private GetUsersListService _sut;


        [SetUp]
        public void SetUp()
        {
            _userManager = new Mock<IUserManager>();

            _sut = new GetUsersListService(_userManager.Object);
        }

        [Test]
        public void GetUsersList_WhenCalled_ReturnUsersListWithTheirIds()
        {
            var user = new User("email", "organizationId");
            var users = new List<User> {user};
            _userManager.Setup(x => x.GetOrganizationUsersAsync()).ReturnsAsync(users);

            var result = _sut.GetUsersList(default).Result;

            Assert.That(result.Count(x => x.Id == user.Id), Is.EqualTo(1));
        }

        [Test]
        public void GetUsersList_WhenCalled_ReturnUsersListWithTheirEmails()
        {
            var user = new User("email", "organizationId");
            var users = new List<User> {user};
            _userManager.Setup(x => x.GetOrganizationUsersAsync()).ReturnsAsync(users);

            var result = _sut.GetUsersList(default).Result;

            Assert.That(result.Count(x => x.Email == user.Email), Is.EqualTo(1));
        }

        [Test]
        public void GetUsersList_WhenCalled_ReturnUsersListWithTheirRoles()
        {
            // Arrange
            var user = new User("email", "organizationId");
            user.AddRole(new Role("roleId1", "role1", "description"));
            user.AddRole(new Role("roleId2", "role2", "description"));
            var users = new List<User> {user};
            _userManager.Setup(x => x.GetOrganizationUsersAsync()).ReturnsAsync(users);

            // Act
            var result = _sut.GetUsersList(default).Result;

            // Assert
            Assert.That(result.Single(x => x.Id == user.Id).Roles, Is.EqualTo("role1, role2"));
        }

        [Test]
        public void GetUsersList_WhenCalled_ReturnUsersListWithTheirStatus()
        {
            var user = new User("email", "organizationId");
            var users = new List<User> {user};
            _userManager.Setup(x => x.GetOrganizationUsersAsync()).ReturnsAsync(users);

            var result = _sut.GetUsersList(default).Result;

            Assert.That(result.Single(x => x.Id == user.Id).Status, Is.EqualTo(user.UserStatusName));
        }

        [Test]
        public void GetUsersList_WhenCalled_ReturnUsersListWithTheirLearningPaths()
        {
            var user = new User("email", "organizationId");
            user.UpdateLearningPath(new LearningPath("name", "organization"));
            var users = new List<User> {user};
            _userManager.Setup(x => x.GetOrganizationUsersAsync()).ReturnsAsync(users);

            var result = _sut.GetUsersList(default).Result;

            Assert.That(result.Count(x => x.LearningPath == user.LearningPath.Name),
                Is.EqualTo(1));
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Identity;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Users.Services.Commands;
using Solen.Core.Domain.Identity.Entities;
using Solen.Core.Domain.Identity.Enums;

namespace UsersManagement.Services.UnitTests.Commands.UpdateUserRoles
{
    [TestFixture]
    public class UpdateUserRolesServiceTests
    {
        private Mock<IUpdateUserRolesRepository> _repo;
        private Mock<IUserManager> _userManager;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;
        private UpdateUserRolesService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IUpdateUserRolesRepository>();
            _userManager = new Mock<IUserManager>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();
            _sut = new UpdateUserRolesService(_repo.Object, _userManager.Object, _currentUserAccessor.Object);

            _currentUserAccessor.Setup(x => x.UserId).Returns("userId");
        }

        [Test]
        public void CheckIfTheUserIsTheCurrentUser_SameUser_ThrowSameUserRolesModificationException()
        {
            Assert.That(() => _sut.CheckIfTheUserIsTheCurrentUser("userId"),
                Throws.TypeOf<SameUserRolesModificationException>());
        }

        [Test]
        public void CheckIfTheUserIsTheCurrentUser_NotTheSameUser_ThrowNoException()
        {
            Assert.That(() => _sut.CheckIfTheUserIsTheCurrentUser("otherUserId"),
                Throws.Nothing);
        }

        [Test]
        public void GetUserFromRepo_TheUserDoesNotExist_ThrowNotFoundException()
        {
            _userManager.Setup(x => x.FindByIdAsync("userId")).ReturnsAsync((User) null);

            Assert.That(() => _sut.GetUserFromRepo("userId", default),
                Throws.TypeOf<NotFoundException>());
        }

        [Test]
        public void GetUserFromRepo_TheUserDoesExist_ReturnUser()
        {
            var user = new User("email", "organizationId");
            _userManager.Setup(x => x.FindByIdAsync("userId")).ReturnsAsync(user);

            var result = _sut.GetUserFromRepo("userId", default).Result;

            Assert.That(result, Is.EqualTo(user));
        }

        [Test]
        public void CheckRole_RoleDoesNotExist_ThrowNotFoundException()
        {
            _repo.Setup(x => x.DoesRoleExist("roleId", default))
                .ReturnsAsync(false);

            Assert.That(() => _sut.CheckRole("roleId", default),
                Throws.TypeOf<NotFoundException>());
        }

        [Test]
        public void CheckRole_RoleDoesExist_ThrowNoException()
        {
            _repo.Setup(x => x.DoesRoleExist("roleId", default))
                .ReturnsAsync(true);

            Assert.That(() => _sut.CheckRole("roleId", default), Throws.Nothing);
        }

        [Test]
        public void RemoveUserRoles_WhenCalled_RemoveTheUserRoles()
        {
            var user = new Mock<User>("email", "organizationId");
            var userRoles = new List<UserRole> {new UserRole("userId", "roleId")};
            user.Setup(x => x.UserRoles).Returns(userRoles);

            _sut.RemoveUserRoles(user.Object);

            user.Verify(x => x.RemoveRoleById(It.IsIn(userRoles.Select(r => r.RoleId))),
                Times.Exactly(userRoles.Count));
        }

        [Test]
        public void DoesAdminRoleIncluded_AdminRoleIsIncluded_ReturnTrue()
        {
            var roles = new List<string> {UserRoles.Admin, UserRoles.Learner};

            var result = _sut.DoesAdminRoleIncluded(roles);

            Assert.That(result, Is.True);
        }

        [Test]
        public void DoesAdminRoleIncluded_AdminRoleIsNotIncluded_ReturnFalse()
        {
            var roles = new List<string> {UserRoles.Instructor, UserRoles.Learner};

            var result = _sut.DoesAdminRoleIncluded(roles);

            Assert.That(result, Is.False);
        }

        [Test]
        public void AddRolesToUser_WhenCalled_RemoveTheUserRoles()
        {
            var user = new Mock<User>("email", "organizationId");
            var roles = new List<string> {"role1", "role2"};

            _sut.AddRolesToUser(user.Object, roles);

            user.Verify(x => x.AddRoleId(It.IsIn<string>(roles)),
                Times.Exactly(roles.Count));
        }
        
        [Test]
        public void UpdateUser_WhenCalled_UpdateTheUserRepo()
        {
            var user = new User("email", "organizationId");

            _sut.UpdateUserRepo(user);

            _userManager.Verify(x => x.UpdateUser(user));
        }
    }
}
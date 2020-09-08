using NUnit.Framework;
using Solen.Core.Domain.Identity.Entities;

namespace Domain.UnitTests.Identity.Entities
{
    [TestFixture]
    public class UserRoleTests
    {
        private UserRole _sut;

        [Test]
        public void ConstructorWithUserIdRoleId_WhenCalled_SetRequiredPropertiesCorrectly()
        {
            _sut = new UserRole("userId", "roleId");

            Assert.That(_sut.UserId, Is.EqualTo("userId"));
            Assert.That(_sut.RoleId, Is.EqualTo("roleId"));
        }
        
        [Test]
        public void ConstructorWithUserIdRole_WhenCalled_SetRequiredPropertiesCorrectly()
        {
            var role = new Role("id", "name", "description");
            _sut = new UserRole("userId", role);

            Assert.That(_sut.UserId, Is.EqualTo("userId"));
            Assert.That(_sut.RoleId, Is.EqualTo(role.Id));
            Assert.That(_sut.Role, Is.EqualTo(role));
        }
    }
}
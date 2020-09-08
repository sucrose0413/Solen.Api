using NUnit.Framework;
using Solen.Core.Domain.Identity.Entities;

namespace Domain.UnitTests.Identity.Entities
{
    [TestFixture]
    public class RoleTests
    {
        private Role _sut;

        [Test]
        public void ConstructorWithIdName_WhenCalled_SetPropertiesCorrectly()
        {
            _sut = new Role("roleId", "role name", "roleDescription");

            Assert.That(_sut.Id, Is.EqualTo("roleId"));
            Assert.That(_sut.Name, Is.EqualTo("role name"));
        }
    }
}
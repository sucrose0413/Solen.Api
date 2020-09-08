using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Identity.Impl;
using Solen.Core.Domain.Identity.Entities;

namespace Common.Identity.UnitTests
{
    [TestFixture]
    public class RoleManagerTests
    {
        private RoleManager _sut;
        private Mock<IRoleManagerRepo> _repo;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IRoleManagerRepo>();

            _sut = new RoleManager(_repo.Object);
        }

        [Test]
        public void GetRoles_WhenCalled_ReturnRolesList()
        {
            var roles = new List<Role>();
            _repo.Setup(x => x.GetRoles()).ReturnsAsync(roles);

            var result = _sut.GetRoles().Result;

            Assert.That(result, Is.EqualTo(roles));
        }

        [Test]
        public void DoesRoleExist_RoleDoesNotExist_ReturnFalse()
        {
            _repo.Setup(x => x.DoesRoleExist("roleId")).ReturnsAsync(false);

            var result = _sut.DoesRoleExist("roleId").Result;

            Assert.That(result, Is.False);
        }

        [Test]
        public void DoesRoleExist_RoleDoesExist_ReturnTrue()
        {
            _repo.Setup(x => x.DoesRoleExist("roleId")).ReturnsAsync(true);

            var result = _sut.DoesRoleExist("roleId").Result;

            Assert.That(result, Is.True);
        }
    }
}
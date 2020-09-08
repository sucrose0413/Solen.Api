using System.Collections.Generic;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.Users.Commands;

namespace UsersManagement.UnitTests.Commands.UpdateUserRoles
{
    [TestFixture]
    public class UpdateUserRolesCommandValidationTests
    {
        private UpdateUserRolesCommandValidator _sut;
        private UpdateUserRolesCommand _command;

        [SetUp]
        public void SetUp()
        {
            _sut = new UpdateUserRolesCommandValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void UserIdIsNullOrEmpty_ShouldHaveError(string userId)
        {
            _command = new UpdateUserRolesCommand {UserId = userId};

            _sut.ShouldHaveValidationErrorFor(x => x.UserId, _command);
        }

        [Test]
        public void UserIdIsValid_ShouldNotHaveError()
        {
            _command = new UpdateUserRolesCommand {UserId = "userId"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.UserId, _command);
        }

        [Test]
        public void RolesIdsListIsNull_ShouldHaveError()
        {
            _command = new UpdateUserRolesCommand {RolesIds = null};

            _sut.ShouldHaveValidationErrorFor(x => x.RolesIds, _command);
        }

        [Test]
        public void RolesIdsListIsEmpty_ShouldHaveError()
        {
            _command = new UpdateUserRolesCommand {RolesIds = new List<string>()};

            _sut.ShouldHaveValidationErrorFor(x => x.RolesIds, _command);
        }

        [Test]
        public void RolesIdsListIsValid_ShouldNotHaveError()
        {
            _command = new UpdateUserRolesCommand {RolesIds = new List<string> {"roleId"}};

            _sut.ShouldNotHaveValidationErrorFor(x => x.RolesIds, _command);
        }
    }
}
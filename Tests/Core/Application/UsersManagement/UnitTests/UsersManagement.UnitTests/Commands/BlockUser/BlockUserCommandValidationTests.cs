using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.Users.Commands;

namespace UsersManagement.UnitTests.Commands.BlockUser
{
    [TestFixture]
    public class BlockUserCommandValidationTests
    {
        private BlockUserCommandValidator _sut;
        private BlockUserCommand _command;

        [SetUp]
        public void SetUp()
        {
            _sut = new BlockUserCommandValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void UserIdIsNullOrEmpty_ShouldHaveError(string userId)
        {
            _command = new BlockUserCommand {UserId = userId};

            _sut.ShouldHaveValidationErrorFor(x => x.UserId, _command);
        }

        [Test]
        public void UserIdIsValid_ShouldNotHaveError()
        {
            _command = new BlockUserCommand {UserId = "userId"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.UserId, _command);
        }
    }
}
using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.Users.Commands;

namespace UsersManagement.UnitTests.Commands.UnblockUser
{
    [TestFixture]
    public class UnblockUserCommandValidationTests
    {
        private UnblockUserCommandValidator _sut;
        private UnblockUserCommand _command;

        [SetUp]
        public void SetUp()
        {
            _sut = new UnblockUserCommandValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void UserIdIsNullOrEmpty_ShouldHaveError(string userId)
        {
            _command = new UnblockUserCommand {UserId = userId};

            _sut.ShouldHaveValidationErrorFor(x => x.UserId, _command);
        }

        [Test]
        public void UserIdIsValid_ShouldNotHaveError()
        {
            _command = new UnblockUserCommand {UserId = "userId"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.UserId, _command);
        }
    }
}
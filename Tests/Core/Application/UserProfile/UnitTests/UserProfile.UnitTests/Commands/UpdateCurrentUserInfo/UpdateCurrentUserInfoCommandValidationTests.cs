using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.UserProfile.Commands;

namespace UserProfile.UnitTests.Commands.UpdateCurrentUserInfo
{
    [TestFixture]
    public class UpdateCurrentUserInfoCommandValidationTests
    {
        private UpdateCurrentUserInfoCommandValidator _sut;
        private UpdateCurrentUserInfoCommand _command;

        [SetUp]
        public void SetUp()
        {
            _sut = new UpdateCurrentUserInfoCommandValidator();
        }
        
        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void UserNameIsNullOrEmpty_ShouldHaveError(string userName)
        {
            _command = new UpdateCurrentUserInfoCommand {UserName = userName};

            _sut.ShouldHaveValidationErrorFor(x => x.UserName, _command);
        }

        [Test]
        public void UserNameLengthIsOver30_ShouldHaveError()
        {
            _command = new UpdateCurrentUserInfoCommand {UserName = new string('*', 31)};

            _sut.ShouldHaveValidationErrorFor(x => x.UserName, _command);
        }

        [Test]
        public void UserNameIsValid_ShouldNotHaveError()
        {
            _command = new UpdateCurrentUserInfoCommand {UserName = "user name"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.UserName, _command);
        }
    }
}
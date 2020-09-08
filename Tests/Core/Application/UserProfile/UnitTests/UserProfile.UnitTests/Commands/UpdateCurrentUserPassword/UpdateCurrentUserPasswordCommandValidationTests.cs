using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.UserProfile.Commands;

namespace UserProfile.UnitTests.Commands.UpdateCurrentUserPassword
{
    [TestFixture]
    public class UpdateCurrentUserPasswordCommandValidationTests
    {
        private UpdateCurrentUserPasswordCommandValidator _sut;
        private UpdateCurrentUserPasswordCommand _command;

        [SetUp]
        public void SetUp()
        {
            _sut = new UpdateCurrentUserPasswordCommandValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void PasswordIsNullOrEmpty_ShouldHaveError(string password)
        {
            _command = new UpdateCurrentUserPasswordCommand {NewPassword = password};

            _sut.ShouldHaveValidationErrorFor(x => x.NewPassword, _command);
        }

        [Test]
        public void PasswordIsDifferentFromConfirmPassword_ShouldHaveError()
        {
            _command = new UpdateCurrentUserPasswordCommand
                {NewPassword = "password", ConfirmNewPassword = "other password"};

            _sut.ShouldHaveValidationErrorFor(x => x.NewPassword, _command);
        }

        [Test]
        public void PasswordIsValid_ShouldNotHaveError()
        {
            _command = new UpdateCurrentUserPasswordCommand
                {NewPassword = "password", ConfirmNewPassword = "password"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.NewPassword, _command);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void ConfirmPasswordIsNullOrEmpty_ShouldHaveError(string confirmPassword)
        {
            _command = new UpdateCurrentUserPasswordCommand {ConfirmNewPassword = confirmPassword};

            _sut.ShouldHaveValidationErrorFor(x => x.ConfirmNewPassword, _command);
        }

        [Test]
        public void ConfirmPasswordIsValid_ShouldNotHaveError()
        {
            _command = new UpdateCurrentUserPasswordCommand {ConfirmNewPassword = "password"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.ConfirmNewPassword, _command);
        }
    }
}
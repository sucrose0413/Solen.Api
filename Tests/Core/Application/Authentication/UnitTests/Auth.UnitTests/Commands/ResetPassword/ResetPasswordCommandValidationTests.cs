using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.Auth.Commands;

namespace Auth.UnitTests.Commands.ResetPassword
{
    [TestFixture]
    public class ResetPasswordCommandValidationTests
    {
        private ResetPasswordCommandValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new ResetPasswordCommandValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void TokenIsNullOrEmpty_ShouldHaveError(string passwordToken)
        {
            var command = new ResetPasswordCommand {PasswordToken = passwordToken};

            _sut.ShouldHaveValidationErrorFor(x => x.PasswordToken, command);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void NewPasswordIsNullOrEmpty_ShouldHaveError(string newPassword)
        {
            var command = new ResetPasswordCommand {NewPassword = newPassword};

            _sut.ShouldHaveValidationErrorFor(x => x.NewPassword, command);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void ConfirmNewPasswordIsNullOrEmpty_ShouldHaveError(string confirmNewPassword)
        {
            var command = new ResetPasswordCommand {ConfirmNewPassword = confirmNewPassword};

            _sut.ShouldHaveValidationErrorFor(x => x.ConfirmNewPassword, command);
        }

        [Test]
        public void PasswordAndConfirmPasswordDoNotMatch_ShouldHaveError()
        {
            var command = new ResetPasswordCommand {NewPassword = "password", ConfirmNewPassword = "anotherPassword"};

            _sut.ShouldHaveValidationErrorFor(x => x.NewPassword, command);
        }
        
        [Test]
        public void ValidCommandData_ShouldNotHaveError()
        {
            var command = new ResetPasswordCommand
            {
                PasswordToken = "token",
                NewPassword = "password", 
                ConfirmNewPassword = "password"
            };

            _sut.ShouldNotHaveValidationErrorFor(x => x.PasswordToken, command);
            _sut.ShouldNotHaveValidationErrorFor(x => x.NewPassword, command);
            _sut.ShouldNotHaveValidationErrorFor(x => x.ConfirmNewPassword, command);
        }
    }
}
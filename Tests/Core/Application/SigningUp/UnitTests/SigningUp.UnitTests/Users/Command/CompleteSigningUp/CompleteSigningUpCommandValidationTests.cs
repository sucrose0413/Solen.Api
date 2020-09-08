using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.SigningUp.Users.Commands;


namespace SigningUp.UnitTests.Users.Command.CompleteSigningUp
{
    [TestFixture]
    public class CompleteSigningUpCommandValidationTests
    {
        private CompleteSigningUpCommandValidator _sut;
        private CompleteUserSigningUpCommand _command;

        [SetUp]
        public void SetUp()
        {
            _sut = new CompleteSigningUpCommandValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void SigningUpTokenIsNullOrEmpty_ShouldHaveError(string token)
        {
            _command = new CompleteUserSigningUpCommand {SigningUpToken = token};

            _sut.ShouldHaveValidationErrorFor(x => x.SigningUpToken, _command);
        }

        [Test]
        public void SigningUpTokenIsValid_ShouldNotHaveError()
        {
            _command = new CompleteUserSigningUpCommand {SigningUpToken = "token"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.SigningUpToken, _command);
        }


        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void UserNameIsNullOrEmpty_ShouldHaveError(string userName)
        {
            _command = new CompleteUserSigningUpCommand {UserName = userName};

            _sut.ShouldHaveValidationErrorFor(x => x.UserName, _command);
        }

        [Test]
        public void UserNameLengthIsOver30_ShouldHaveError()
        {
            _command = new CompleteUserSigningUpCommand {UserName = new string('*', 31)};

            _sut.ShouldHaveValidationErrorFor(x => x.UserName, _command);
        }

        [Test]
        public void UserNameIsValid_ShouldNotHaveError()
        {
            _command = new CompleteUserSigningUpCommand {UserName = "user name"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.UserName, _command);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void PasswordIsNullOrEmpty_ShouldHaveError(string password)
        {
            _command = new CompleteUserSigningUpCommand {Password = password};

            _sut.ShouldHaveValidationErrorFor(x => x.Password, _command);
        }

        [Test]
        public void PasswordIsDifferentFromConfirmPassword_ShouldHaveError()
        {
            _command = new CompleteUserSigningUpCommand
                {Password = "password", ConfirmPassword = "other password"};

            _sut.ShouldHaveValidationErrorFor(x => x.Password, _command);
        }

        [Test]
        public void PasswordIsValid_ShouldNotHaveError()
        {
            _command = new CompleteUserSigningUpCommand
                {Password = "password", ConfirmPassword = "password"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.Password, _command);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void ConfirmPasswordIsNullOrEmpty_ShouldHaveError(string confirmPassword)
        {
            _command = new CompleteUserSigningUpCommand {ConfirmPassword = confirmPassword};

            _sut.ShouldHaveValidationErrorFor(x => x.ConfirmPassword, _command);
        }

        [Test]
        public void ConfirmPasswordIsValid_ShouldNotHaveError()
        {
            _command = new CompleteUserSigningUpCommand {ConfirmPassword = "password"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.ConfirmPassword, _command);
        }
    }
}
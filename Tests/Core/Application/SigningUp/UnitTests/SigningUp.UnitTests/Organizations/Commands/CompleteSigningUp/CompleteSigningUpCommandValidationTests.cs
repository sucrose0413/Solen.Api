using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.SigningUp.Organizations.Commands;

namespace SigningUp.UnitTests.Organizations.Commands.CompleteSigningUp
{
    [TestFixture]
    public class CompleteSigningUpCommandValidationTests
    {
        private CompleteSigningUpCommandValidator _sut;
        private CompleteOrganizationSigningUpCommand _command;

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
            _command = new CompleteOrganizationSigningUpCommand {SigningUpToken = token};

            _sut.ShouldHaveValidationErrorFor(x => x.SigningUpToken, _command);
        }

        [Test]
        public void SigningUpTokenIsValid_ShouldNotHaveError()
        {
            _command = new CompleteOrganizationSigningUpCommand {SigningUpToken = "token"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.SigningUpToken, _command);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void OrganizationNameIsNullOrEmpty_ShouldHaveError(string organizationName)
        {
            _command = new CompleteOrganizationSigningUpCommand {OrganizationName = organizationName};

            _sut.ShouldHaveValidationErrorFor(x => x.OrganizationName, _command);
        }

        [Test]
        public void OrganizationNameLengthIsOver60_ShouldHaveError()
        {
            _command = new CompleteOrganizationSigningUpCommand {OrganizationName = new string('*', 61)};

            _sut.ShouldHaveValidationErrorFor(x => x.OrganizationName, _command);
        }

        [Test]
        public void OrganizationNameIsValid_ShouldNotHaveError()
        {
            _command = new CompleteOrganizationSigningUpCommand {OrganizationName = "organization name"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.OrganizationName, _command);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void UserNameIsNullOrEmpty_ShouldHaveError(string userName)
        {
            _command = new CompleteOrganizationSigningUpCommand {UserName = userName};

            _sut.ShouldHaveValidationErrorFor(x => x.UserName, _command);
        }

        [Test]
        public void UserNameLengthIsOver30_ShouldHaveError()
        {
            _command = new CompleteOrganizationSigningUpCommand {UserName = new string('*', 31)};

            _sut.ShouldHaveValidationErrorFor(x => x.UserName, _command);
        }

        [Test]
        public void UserNameIsValid_ShouldNotHaveError()
        {
            _command = new CompleteOrganizationSigningUpCommand {UserName = "user name"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.UserName, _command);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void PasswordIsNullOrEmpty_ShouldHaveError(string password)
        {
            _command = new CompleteOrganizationSigningUpCommand {Password = password};

            _sut.ShouldHaveValidationErrorFor(x => x.Password, _command);
        }

        [Test]
        public void PasswordIsDifferentFromConfirmPassword_ShouldHaveError()
        {
            _command = new CompleteOrganizationSigningUpCommand
                {Password = "password", ConfirmPassword = "other password"};

            _sut.ShouldHaveValidationErrorFor(x => x.Password, _command);
        }
        
        [Test]
        public void PasswordIsValid_ShouldNotHaveError()
        {
            _command = new CompleteOrganizationSigningUpCommand
                {Password = "password", ConfirmPassword = "password"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.Password, _command);
        }
        
        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void ConfirmPasswordIsNullOrEmpty_ShouldHaveError(string confirmPassword)
        {
            _command = new CompleteOrganizationSigningUpCommand {ConfirmPassword = confirmPassword};

            _sut.ShouldHaveValidationErrorFor(x => x.ConfirmPassword, _command);
        }
        
        [Test]
        public void ConfirmPasswordIsValid_ShouldNotHaveError()
        {
            _command = new CompleteOrganizationSigningUpCommand {ConfirmPassword = "password"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.ConfirmPassword, _command);
        }
    }
}
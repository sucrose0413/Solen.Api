using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.SigningUp.Organizations.Commands;

namespace SigningUp.UnitTests.Organizations.Commands.InitSigningUp
{
    [TestFixture]
    public class InitSigningUpCommandValidationTests
    {
        private InitSigningUpCommandValidator _sut;
        private InitSigningUpCommand _command;

        [SetUp]
        public void SetUp()
        {
            _sut = new InitSigningUpCommandValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("invalidEmail")]
        public void EmailIsNullOrEmptyOrInvalid_ShouldHaveError(string email)
        {
            _command = new InitSigningUpCommand {Email = email};

            _sut.ShouldHaveValidationErrorFor(x => x.Email, _command);
        }

        [Test]
        public void EmailIsValid_ShouldNotHaveError()
        {
            _command = new InitSigningUpCommand {Email = "email@example.com"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.Email, _command);
        }
    }
}
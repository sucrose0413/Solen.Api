using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.Auth.Commands;

namespace Auth.UnitTests.Commands.ForgotPassword
{
    [TestFixture]
    public class ForgotPasswordCommandValidationTests
    {
        private ForgotPasswordCommandValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new ForgotPasswordCommandValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void EmptyOrNullEmail_ShouldHaveError(string email)
        {
            var command = new ForgotPasswordCommand {Email = email};

            _sut.ShouldHaveValidationErrorFor(x => x.Email, command);
        }

        [Test]
        public void InvalidEmail_ShouldHaveError()
        {
            var command = new ForgotPasswordCommand {Email = "invalidEmail"};

            _sut.ShouldHaveValidationErrorFor(x => x.Email, command);
        }

        [Test]
        public void ValidLoginAndPassword_ShouldNotHaveErrors()
        {
            var command = new ForgotPasswordCommand {Email = "email@example.com"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.Email, command);
        }
    }
}
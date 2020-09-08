using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.Auth.Queries;

namespace Auth.UnitTests.Queries.CheckPasswordToken
{
    [TestFixture]
    public class CheckPasswordTokenQueryValidationTests
    {
        private CheckPasswordTokenQueryValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new CheckPasswordTokenQueryValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void PasswordTokenIsNullOrEmpty_ShouldHaveError(string token)
        {
            var query = new CheckPasswordTokenQuery {PasswordToken = token};

            _sut.ShouldHaveValidationErrorFor(x => x.PasswordToken, query);
        }

        [Test]
        public void PasswordTokenIsValid_ShouldNotHaveError()
        {
            var query = new CheckPasswordTokenQuery {PasswordToken = "password token"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.PasswordToken, query);
        }
    }
}
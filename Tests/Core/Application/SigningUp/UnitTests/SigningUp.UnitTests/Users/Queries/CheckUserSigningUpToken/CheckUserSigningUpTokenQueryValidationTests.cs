using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.SigningUp.Users.Queries;

namespace SigningUp.UnitTests.Users.Queries.CheckUserSigningUpToken
{
    [TestFixture]
    public class CheckUserSigningUpTokenQueryValidationTests
    {
        private CheckUserSigningUpTokenQueryValidator _sut;
        private CheckUserSigningUpTokenQuery _query;

        [SetUp]
        public void SetUp()
        {
            _sut = new CheckUserSigningUpTokenQueryValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void SigningUpTokenIsNullOrEmpty_ShouldHaveError(string token)
        {
            _query = new CheckUserSigningUpTokenQuery {SigningUpToken = token};

            _sut.ShouldHaveValidationErrorFor(x => x.SigningUpToken, _query);
        }

        [Test]
        public void SigningUpTokenIsValid_ShouldNotHaveError()
        {
            _query = new CheckUserSigningUpTokenQuery {SigningUpToken = "token"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.SigningUpToken, _query);
        }
    }
}
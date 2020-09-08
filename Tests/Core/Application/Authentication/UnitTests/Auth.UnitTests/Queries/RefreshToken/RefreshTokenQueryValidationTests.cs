using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.Auth.Queries;

namespace Auth.UnitTests.Queries.RefreshToken
{
    [TestFixture]
    public class RefreshTokenQueryValidationTests
    {
        private RefreshTokenQueryValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new RefreshTokenQueryValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void RefreshTokenIsNullOrEmpty_ShouldHaveError(string token)
        {
            var query = new RefreshTokenQuery(token);

            _sut.ShouldHaveValidationErrorFor(x => x.RefreshToken, query);
        }

        [Test]
        public void RefreshTokenIsValid_ShouldNotHaveError()
        {
            var query = new RefreshTokenQuery("refresh token");

            _sut.ShouldNotHaveValidationErrorFor(x => x.RefreshToken, query);
        }
    }
}
using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.SigningUp.Organizations.Queries;

namespace SigningUp.UnitTests.Organizations.Queries.CheckOrganizationSigningUpToken
{
    [TestFixture]
    public class CheckOrganizationSigningUpTokenQueryValidationTests
    {
        private CheckOrganizationSigningUpTokenQueryValidator _sut;
        private CheckOrganizationSigningUpTokenQuery _query;

        [SetUp]
        public void SetUp()
        {
            _sut = new CheckOrganizationSigningUpTokenQueryValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void SigningUpTokenIsNullOrEmpty_ShouldHaveError(string token)
        {
            _query = new CheckOrganizationSigningUpTokenQuery {SigningUpToken = token};

            _sut.ShouldHaveValidationErrorFor(x => x.SigningUpToken, _query);
        }

        [Test]
        public void SigningUpTokenIsValid_ShouldNotHaveError()
        {
            _query = new CheckOrganizationSigningUpTokenQuery {SigningUpToken = "token"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.SigningUpToken, _query);
        }
    }
}
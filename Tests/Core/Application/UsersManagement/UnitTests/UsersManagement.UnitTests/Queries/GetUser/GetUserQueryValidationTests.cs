using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.Users.Queries;

namespace UsersManagement.UnitTests.Queries.GetUser
{
    [TestFixture]
    public class GetUserQueryValidationTests
    {
        private GetUserQueryValidator _sut;
        private GetUserQuery _query;

        [SetUp]
        public void SetUp()
        {
            _sut = new GetUserQueryValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void UserIdIsNullOrEmpty_ShouldHaveError(string userId)
        {
            _query = new GetUserQuery(userId);

            _sut.ShouldHaveValidationErrorFor(x => x.UserId, _query);
        }

        [Test]
        public void UserIdIsValid_ShouldNotHaveError()
        {
            _query = new GetUserQuery("userId");

            _sut.ShouldNotHaveValidationErrorFor(x => x.UserId, _query);
        }
    }
}
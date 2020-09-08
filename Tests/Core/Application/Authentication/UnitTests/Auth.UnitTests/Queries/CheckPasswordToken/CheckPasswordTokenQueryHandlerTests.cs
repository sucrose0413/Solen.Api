using Moq;
using NUnit.Framework;
using Solen.Core.Application.Auth.Queries;

namespace Auth.UnitTests.Queries.CheckPasswordToken
{
    [TestFixture]
    public class CheckPasswordTokenQueryHandlerTests
    {
        private CheckPasswordTokenQueryHandler _sut;
        private Mock<ICheckPasswordTokenService> _service;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<ICheckPasswordTokenService>();
            _sut = new CheckPasswordTokenQueryHandler(_service.Object);
        }

        [Test]
        public void WhenCalled_CheckIfUserIsBlockedOrInactive()
        {
            var query = new CheckPasswordTokenQuery {PasswordToken = "password token"};

            _sut.Handle(query, default).Wait();

            _service.Verify(x => x.CheckPasswordToken(query.PasswordToken, default));
        }
    }
}
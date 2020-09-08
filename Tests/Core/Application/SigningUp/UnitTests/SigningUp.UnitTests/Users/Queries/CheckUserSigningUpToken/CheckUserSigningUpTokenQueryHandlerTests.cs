using Moq;
using NUnit.Framework;
using Solen.Core.Application.SigningUp.Users.Queries;

namespace SigningUp.UnitTests.Users.Queries.CheckUserSigningUpToken
{
    [TestFixture]
    public class CheckUserSigningUpTokenQueryHandlerTests
    {
        private Mock<ICheckUserSigningUpTokenService> _service;
        private CheckUserSigningUpTokenQuery _query;
        private CheckUserSigningUpTokenQueryHandler _sut;


        [SetUp]
        public void SetUp()
        {
            _service = new Mock<ICheckUserSigningUpTokenService>();
            _query = new CheckUserSigningUpTokenQuery {SigningUpToken = "token"};
            _sut = new CheckUserSigningUpTokenQueryHandler(_service.Object);
        }
        
        [Test]
        public void WhenCalled_CheckSigningUpToken()
        {
            _sut.Handle(_query, default).Wait();

            _service.Verify(x => x.CheckSigningUpToken(_query.SigningUpToken, default));
        }
    }
}
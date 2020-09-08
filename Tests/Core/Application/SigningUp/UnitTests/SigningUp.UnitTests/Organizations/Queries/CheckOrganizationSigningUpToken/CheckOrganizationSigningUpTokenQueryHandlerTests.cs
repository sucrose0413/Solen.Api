using Moq;
using NUnit.Framework;
using Solen.Core.Application.SigningUp.Organizations.Queries;

namespace SigningUp.UnitTests.Organizations.Queries.CheckOrganizationSigningUpToken
{
    [TestFixture]
    public class CheckOrganizationSigningUpTokenQueryHandlerTests
    {
        private Mock<ICheckOrganizationSigningUpTokenService> _service;
        private CheckOrganizationSigningUpTokenQuery _query;
        private CheckOrganizationSigningUpTokenQueryHandler _sut;


        [SetUp]
        public void SetUp()
        {
            _service = new Mock<ICheckOrganizationSigningUpTokenService>();
            _query = new CheckOrganizationSigningUpTokenQuery {SigningUpToken = "token"};
            _sut = new CheckOrganizationSigningUpTokenQueryHandler(_service.Object);
        }
        
        [Test]
        public void WhenCalled_CheckSigningUpToken()
        {
            _sut.Handle(_query, default).Wait();

            _service.Verify(x => x.CheckSigningUpToken(_query.SigningUpToken, default));
        }
    }
}
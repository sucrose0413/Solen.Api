using Moq;
using NUnit.Framework;
using Solen.Core.Application.Auth.Queries;
using Solen.Core.Domain.Identity.Entities;

namespace Auth.UnitTests.Queries.GetCurrentLoggedUser
{
    [TestFixture]
    public class GetCurrentLoggedUserQueryHandlerTests
    {
        private GetCurrentLoggedUserQueryHandler _sut;
        private Mock<IGetCurrentLoggedUserService> _service;
        private Mock<ICommonService> _commonService;
        private GetCurrentLoggedUserQuery _query;
        private User _user;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IGetCurrentLoggedUserService>();
            _commonService = new Mock<ICommonService>();
            _sut = new GetCurrentLoggedUserQueryHandler(_service.Object, _commonService.Object);
            _query = new GetCurrentLoggedUserQuery();

            _user = new User("email", "organizationId");
            _service.Setup(x => x.GetCurrentUser(default))
                .ReturnsAsync(_user);
        }

        [Test]
        public void WhenCalled_CheckIfUserIsBlockedOrInactive()
        {
            _sut.Handle(_query, default).Wait();

            _commonService.Verify(x => x.CheckIfUserIsBlockedOrInactive(_user));
        }

        [Test]
        public void ActiveUser_ReturnLoggedUserToken()
        {
            var loggedUser = new LoggedUserDto();
            _commonService.Setup(x => x.CreateLoggedUser(_user))
                .Returns(loggedUser);

            var result = _sut.Handle(_query, default).Result;

            Assert.That(result, Is.EqualTo(loggedUser));
        }
    }
}
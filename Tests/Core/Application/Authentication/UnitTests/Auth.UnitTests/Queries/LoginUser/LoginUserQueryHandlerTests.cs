using Moq;
using NUnit.Framework;
using Solen.Core.Application.Auth.Queries;
using Solen.Core.Application.UnitOfWork;
using Solen.Core.Domain.Identity.Entities;
using RefreshTokenEntity = Solen.Core.Domain.Security.Entities.RefreshToken;

namespace Auth.UnitTests.Queries.LoginUser
{
    [TestFixture]
    public class LoginUserQueryHandlerTests
    {
        private LoginUserQueryHandler _sut;
        private Mock<ILoginUserService> _service;
        private Mock<ICommonService> _commonService;
        private Mock<IUnitOfWork> _unitOfWork;

        private LoginUserQuery _query;
        private User _user;
        private RefreshTokenEntity _refreshToken;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<ILoginUserService>();
            _commonService = new Mock<ICommonService>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _sut = new LoginUserQueryHandler(_service.Object, _commonService.Object, _unitOfWork.Object);
            _query = new LoginUserQuery {Email = "email", Password = "password"};

            _user = new User("email", "organizationId");
            _service.Setup(x => x.GetUserByEmail(_query.Email, default))
                .ReturnsAsync(_user);
        }


        [Test]
        public void WhenCalled_CheckIfPasswordIsInvalid()
        {
            _sut.Handle(_query, default).Wait();

            _service.Verify(x => x.CheckIfPasswordIsInvalid(_user, _query.Password));
        }

        [Test]
        public void WhenCalled_CheckIfUserIsBlockedOrInactive()
        {
            _sut.Handle(_query, default).Wait();

            _commonService.Verify(x => x.CheckIfUserIsBlockedOrInactive(_user));
        }

        [Test]
        public void WhenCalled_RemoveAnyUserRefreshToken()
        {
            _sut.Handle(_query, default).Wait();

            _commonService.Verify(x => x.RemoveAnyUserRefreshToken(_user, default));
        }

        [Test]
        public void WhenCalled_CreateAndAddNewRefreshTokenToRepo()
        {
            var refreshToken = new RefreshTokenEntity(_user, null);
            _commonService.Setup(x => x.CreateNewRefreshToken(_user)).Returns(refreshToken);

            _sut.Handle(_query, default).Wait();

            _commonService.Verify(x => x.AddNewRefreshTokenToRepo(refreshToken));
        }


        [Test]
        public void ValidUserCredentialsAndStatus_ReturnLoggedUser()
        {
            var loggedUser = new LoggedUserDto();
            _commonService.Setup(x => x.CreateLoggedUser(_user))
                .Returns(loggedUser);

            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.LoggedUser, Is.EqualTo(loggedUser));
        }

        [Test]
        public void ValidUserCredentialsAndStatus_ReturnUserToken()
        {
            _commonService.Setup(x => x.CreateUserToken(_user))
                .Returns("token");

            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.Token, Is.EqualTo("token"));
        }

        [Test]
        public void ValidUserCredentialsAndStatus_ReturnRefreshToken()
        {
            _refreshToken = new RefreshTokenEntity(_user, null);
            _commonService.Setup(x => x.CreateNewRefreshToken(_user))
                .Returns(_refreshToken);

            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.RefreshToken, Is.EqualTo(_refreshToken.Id));
        }
    }
}
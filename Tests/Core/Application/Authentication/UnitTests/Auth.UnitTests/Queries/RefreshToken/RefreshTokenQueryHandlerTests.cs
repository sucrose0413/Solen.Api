using Moq;
using NUnit.Framework;
using Solen.Core.Application.Auth.Queries;
using Solen.Core.Application.UnitOfWork;
using Solen.Core.Domain.Identity.Entities;
using RefreshTokenEntity = Solen.Core.Domain.Security.Entities.RefreshToken;

namespace Auth.UnitTests.Queries.RefreshToken
{
    [TestFixture]
    public class RefreshTokenQueryHandlerTests
    {
        private RefreshTokenQueryHandler _sut;
        private Mock<IRefreshTokenService> _service;
        private Mock<ICommonService> _commonService;
        private Mock<IUnitOfWork> _unitOfWork;

        private RefreshTokenQuery _query;
        private User _user;
        private RefreshTokenEntity _currentRefreshToken;
        private RefreshTokenEntity _newRefreshToken;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IRefreshTokenService>();
            _commonService = new Mock<ICommonService>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _sut = new RefreshTokenQueryHandler(_service.Object, _commonService.Object, _unitOfWork.Object);

            _query = new RefreshTokenQuery("currentRefreshToken");

            SetUpFields();
        }

        [Test]
        public void WhenCalled_CheckRefreshTokenValidity()
        {
            _sut.Handle(_query, default).Wait();

            _service.Verify(x => x.CheckRefreshTokenValidity(_currentRefreshToken));
        }

        [Test]
        public void RefreshTokenStillValid_CheckIfUserIsBlockedOrInactive()
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
        public void UserIsActive_SaveChanges()
        {
            _sut.Handle(_query, default).Wait();

            _unitOfWork.Verify(x => x.SaveAsync(default));
        }

        [Test]
        public void UserIsActive_ReturnLoggedUser()
        {
            var loggedUser = new LoggedUserDto();
            _commonService.Setup(x => x.CreateLoggedUser(_user)).Returns(loggedUser);

            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.LoggedUser, Is.EqualTo(loggedUser));
        }

        [Test]
        public void UserIsActive_ReturnUserToken()
        {
            _commonService.Setup(x => x.CreateUserToken(_user)).Returns("token");

            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.Token, Is.EqualTo("token"));
        }

        [Test]
        public void UserIsActive_ReturnNewRefreshToken()
        {
            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.RefreshToken, Is.EqualTo(_newRefreshToken.Id));
        }

        #region Private Methods

        private void SetUpFields()
        {
            _user = new User("email", "organizationId");
            _service.Setup(x => x.GetUserByRefreshToken(_query.RefreshToken, default))
                .ReturnsAsync(_user);
            _currentRefreshToken = new RefreshTokenEntity(_user, null);
            _service.Setup(x => x.GetCurrentRefreshToken(_query.RefreshToken, default))
                .ReturnsAsync(_currentRefreshToken);
            _newRefreshToken = new RefreshTokenEntity(_user, null);
            _commonService.Setup(x => x.CreateNewRefreshToken(_user))
                .Returns(_newRefreshToken);
        }

        #endregion
    }
}
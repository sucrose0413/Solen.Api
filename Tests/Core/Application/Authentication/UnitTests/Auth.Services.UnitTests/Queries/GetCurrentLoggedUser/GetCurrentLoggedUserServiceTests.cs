using Moq;
using NUnit.Framework;
using Solen.Core.Application.Auth.Services.Queries;
using Solen.Core.Application.Common.Identity;
using Solen.Core.Application.Common.Security;
using Solen.Core.Domain.Identity.Entities;

namespace Auth.Services.UnitTests.Queries.GetCurrentLoggedUser
{
    [TestFixture]
    public class GetCurrentLoggedUserServiceTests
    {
        private GetCurrentLoggedUserService _sut;
        private Mock<IUserManager> _userManager;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;

        [SetUp]
        public void SetUp()
        {
            _userManager = new Mock<IUserManager>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();

            _sut = new GetCurrentLoggedUserService(_userManager.Object, _currentUserAccessor.Object);
        }
        
        [Test]
        public void GetCurrentUser_WhenCalled_GetCurrentUser()
        {
            var userId = "userId";
            _currentUserAccessor.Setup(x => x.UserId).Returns(userId);
            var user = new User(userId, "organizationId");
            _userManager.Setup(x => x.FindByIdAsync(userId)).ReturnsAsync(user);

            var result = _sut.GetCurrentUser(default).Result;

            Assert.That(result, Is.EqualTo(user));
        }
    }
}
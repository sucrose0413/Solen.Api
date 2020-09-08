using Moq;
using NUnit.Framework;
using Solen.Core.Application.UserProfile.Queries;

namespace UserProfile.UnitTests.Queries.GetUserInfo
{
    [TestFixture]
    public class GetUserInfoQueryHandlerTests
    {
        private Mock<IGetCurrentUserInfoService> _service;
        private GetCurrentUserInfoQueryHandler _sut;
        private GetUserProfileQuery _query;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IGetCurrentUserInfoService>();
            _sut = new GetCurrentUserInfoQueryHandler(_service.Object);
            _query = new GetUserProfileQuery();
        }


        [Test]
        public void WhenCalled_ReturnCurrentUserInfo()
        {
            var currentUserInfo = new UserForProfileDto();
            _service.Setup(x => x.GetCurrentUserInfo(default))
                .ReturnsAsync(currentUserInfo);

            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.CurrentUser, Is.EqualTo(currentUserInfo));
        }
    }
}
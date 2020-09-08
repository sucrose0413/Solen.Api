using Moq;
using NUnit.Framework;
using Solen.Core.Application.Dashboard.Queries;

namespace Dashboard.UnitTests.Queries.GetUserCountInfo
{
    [TestFixture]
    public class GetUserCountInfoQueryHandlerTests
    {
        private Mock<IGetUserCountInfoService> _service;
        private GetUserCountInfoQueryHandler _sut;
        private GetUserCountInfoQuery _query;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IGetUserCountInfoService>();
            _sut = new GetUserCountInfoQueryHandler(_service.Object);
            _query = new GetUserCountInfoQuery();
        }
        
        [Test]
        public void WhenCalled_ReturnUserCountInfo()
        {
            var userCountInfo = new UserCountInfoDto();
            _service.Setup(x => x.GetUserCountInfo(default))
                .ReturnsAsync(userCountInfo);

            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.UserCountInfo, Is.EqualTo(userCountInfo));
        }
    }
}
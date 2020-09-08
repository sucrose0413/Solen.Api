using Moq;
using NUnit.Framework;
using Solen.Core.Application.Dashboard.Queries;

namespace Dashboard.UnitTests.Queries.GetStorageInfo
{
    [TestFixture]
    public class GetStorageInfoQueryHandlerTests
    {
        private Mock<IGetStorageInfoService> _service;
        private GetStorageInfoQueryHandler _sut;
        private GetStorageInfoQuery _query;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IGetStorageInfoService>();
            _sut = new GetStorageInfoQueryHandler(_service.Object);
            _query = new GetStorageInfoQuery();
        }

        [Test]
        public void WhenCalled_ReturnStorageInfo()
        {
            var storageInfo = new StorageInfoDto();
            _service.Setup(x => x.GetStorageInfo(default))
                .ReturnsAsync(storageInfo);

            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.StorageInfo, Is.EqualTo(storageInfo));
        }
    }
}
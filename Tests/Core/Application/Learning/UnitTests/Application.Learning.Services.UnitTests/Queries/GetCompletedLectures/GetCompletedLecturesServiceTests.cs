using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Learning.Services.Queries;

namespace Application.Learning.Services.UnitTests.Queries.GetCompletedLectures
{
    [TestFixture]
    public class GetCompletedLecturesServiceTests
    {
        private Mock<IGetCompletedLecturesRepository> _repo;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;
        private GetCompletedLecturesService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IGetCompletedLecturesRepository>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();
            _sut = new GetCompletedLecturesService(_repo.Object, _currentUserAccessor.Object);

            _currentUserAccessor.Setup(x => x.UserId).Returns("learnerId");
        }

        [Test]
        public void GetLearnerCompletedLectures_WhenCalled_ReturnCompletedLecturesIdsList()
        {
            var completedLecturesIds = new List<string>();
            _repo.Setup(x => x.GetLearnerCompletedLectures("courseId", "learnerId", default))
                .ReturnsAsync(completedLecturesIds);

            var result = _sut.GetLearnerCompletedLectures("courseId", default).Result;

            Assert.That(result, Is.EqualTo(completedLecturesIds));
        }
    }
}
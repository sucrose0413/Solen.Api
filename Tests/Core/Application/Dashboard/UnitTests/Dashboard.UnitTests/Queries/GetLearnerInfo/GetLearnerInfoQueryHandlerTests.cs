using System;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.Dashboard.Queries;
using Solen.Core.Domain.Courses.Entities;

namespace Dashboard.UnitTests.Queries.GetLearnerInfo
{
    [TestFixture]
    public class GetLearnerInfoQueryHandlerTests
    {
        private Mock<IGetLearnerInfoService> _service;
        private GetLearnerInfoQueryHandler _sut;
        private GetLearnerInfoQuery _query;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IGetLearnerInfoService>();
            _sut = new GetLearnerInfoQueryHandler(_service.Object);
            _query = new GetLearnerInfoQuery();
        }

        [Test]
        public void WhenCalled_ReturnLearnerLastCourseProgress()
        {
            var lastCourse = new Course("title", "creator", DateTime.Now);
            _service.Setup(x => x.GetLastCourse(default)).ReturnsAsync(lastCourse);
            var lastCourseProgress = new LearnerLastCourseProgressDto();
            _service.Setup(x => x.GetLastCourseProgress(lastCourse, default))
                .ReturnsAsync(lastCourseProgress);

            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.LastCourseProgress, Is.EqualTo(lastCourseProgress));
        }
    }
}
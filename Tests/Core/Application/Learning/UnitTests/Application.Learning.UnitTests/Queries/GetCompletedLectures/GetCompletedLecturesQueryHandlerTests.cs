using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.Learning.Queries;

namespace Application.Learning.UnitTests.Queries.GetCompletedLectures
{
    [TestFixture]
    public class GetCompletedLecturesQueryHandlerTests
    {
        private Mock<IGetCompletedLecturesService> _service;
        private GetCompletedLecturesQueryHandler _sut;
        private GetCompletedLecturesQuery _query;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IGetCompletedLecturesService>();
            _sut = new GetCompletedLecturesQueryHandler(_service.Object);
            _query = new GetCompletedLecturesQuery("courseId");
        }

        [Test]
        public void WhenCalled_ReturnCorrectCompletedLecturesIds()
        {
            var completedLecturesIds = new List<string>();
            _service.Setup(x => x.GetLearnerCompletedLectures(_query.CourseId, default))
                .ReturnsAsync(completedLecturesIds);

            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.CompletedLectures, Is.EqualTo(completedLecturesIds));
        }
    }
}
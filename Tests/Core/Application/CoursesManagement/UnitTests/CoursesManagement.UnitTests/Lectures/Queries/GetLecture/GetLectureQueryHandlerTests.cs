using Moq;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Common;
using Solen.Core.Application.CoursesManagement.Lectures.Queries;

namespace CoursesManagement.UnitTests.Lectures.Queries.GetLecture
{
    [TestFixture]
    public class GetLectureQueryHandlerTests
    {
        private GetLectureQueryHandler _sut;
        private GetLectureQuery _query;
        private Mock<IGetLectureService> _service;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IGetLectureService>();
            _sut = new GetLectureQueryHandler(_service.Object);
            _query = new GetLectureQuery {LectureId = "lectureId"};
        }


        [Test]
        public void WhenCalled_ReturnCorrectLecture()
        {
            var lecture = new LectureDto();
            _service.Setup(x => x.GetLecture(_query.LectureId, default)).ReturnsAsync(lecture);

            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.Lecture, Is.EqualTo(lecture));
        }
    }
}
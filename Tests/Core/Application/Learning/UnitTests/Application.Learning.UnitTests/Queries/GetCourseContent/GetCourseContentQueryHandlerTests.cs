using Moq;
using NUnit.Framework;
using Solen.Core.Application.Learning.Queries;

namespace Application.Learning.UnitTests.Queries.GetCourseContent
{
    [TestFixture]
    public class GetCourseContentQueryHandlerTests
    {
        private Mock<IGetCourseContentService> _service;
        private GetCourseContentQueryHandler _sut;
        private GetCourseContentQuery _query;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IGetCourseContentService>();
            _sut = new GetCourseContentQueryHandler(_service.Object);
            _query = new GetCourseContentQuery {CourseId = "courseId"};
        }


        [Test]
        public void WhenCalled_ReturnCorrectCourseContent()
        {
            var courseContent = new LearnerCourseContentDto();
            _service.Setup(x => x.GetCourseContentFromRepo(_query.CourseId, default))
                .ReturnsAsync(courseContent);

            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.CourseContent, Is.EqualTo(courseContent));
        }


        [Test]
        public void WhenCalled_ReturnCorrectLastLectureId()
        {
            const string lastLectureId = "";
            _service.Setup(x => x.GetLastLectureId(_query.CourseId,
                default)).ReturnsAsync(lastLectureId);

            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.LastLectureId, Is.EqualTo(lastLectureId));
        }
    }
}
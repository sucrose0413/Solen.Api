using Moq;
using NUnit.Framework;
using Solen.Core.Application.Dashboard.Queries;

namespace Dashboard.UnitTests.Queries.GetCoursesInfo
{
    [TestFixture]
    public class GetCoursesInfoQueryHandlerTests
    {
        private Mock<IGetCoursesInfoService> _service;
        private GetCoursesInfoQueryHandler _sut;
        private GetCoursesInfoQuery _query;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IGetCoursesInfoService>();
            _sut = new GetCoursesInfoQueryHandler(_service.Object);
            _query = new GetCoursesInfoQuery();
        }

        [Test]
        public void WhenCalled_ReturnCorrectCourseCount()
        {
            var courseCount = 1;
            _service.Setup(x => x.GetCourseCount(default))
                .ReturnsAsync(courseCount);

            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.CourseCount, Is.EqualTo(courseCount));
        }

        [Test]
        public void WhenCalled_ReturnCorrectLastCreatedCourse()
        {
            var lastCreatedCourse = new LastCreatedCourseDto();
            _service.Setup(x => x.GetLastCreatedCourse(default))
                .ReturnsAsync(lastCreatedCourse);

            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.LastCreatedCourse, Is.EqualTo(lastCreatedCourse));
        }

        [Test]
        public void WhenCalled_ReturnCorrectLastPublishedCourse()
        {
            var lastPublishedCourse = new LastPublishedCourseDto();
            _service.Setup(x => x.GetLastPublishedCourse(default))
                .ReturnsAsync(lastPublishedCourse);

            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.LastPublishedCourse, Is.EqualTo(lastPublishedCourse));
        }
    }
}
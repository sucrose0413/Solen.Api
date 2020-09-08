using Moq;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Common;
using Solen.Core.Application.CoursesManagement.Courses.Queries;

namespace CoursesManagement.UnitTests.Courses.Queries.GetCourseContent
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
            var courseContent = new CourseContentDto();
            _service.Setup(x => x.GetCourseContent(_query.CourseId, default))
                .ReturnsAsync(courseContent);

            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.CourseContent, Is.EqualTo(courseContent));
        }
    }
}
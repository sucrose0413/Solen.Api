using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Common;
using Solen.Core.Application.CoursesManagement.Courses.Queries;

namespace CoursesManagement.UnitTests.Courses.Queries.GetCourse
{
    [TestFixture]
    public class GetCourseQueryHandlerTests
    {
        private Mock<IGetCourseService> _service;
        private GetCourseQueryHandler _sut;
        private GetCourseQuery _query;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IGetCourseService>();
            _sut = new GetCourseQueryHandler(_service.Object);
            _query = new GetCourseQuery {CourseId = "courseId"};
        }


        [Test]
        public void WhenCalled__ReturnCorrectCourse()
        {
            var course = new CourseDto();
            _service.Setup(x => x.GetCourse(_query.CourseId, default)).ReturnsAsync(course);

            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.Course, Is.EqualTo(course));
        }

        [Test]
        public void WhenCalled__CourseErrors()
        {
            var courseErrors = new List<CourseErrorDto>();
            _service.Setup(x => x.GetCourseErrors(_query.CourseId, default))
                .ReturnsAsync(courseErrors);

            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.CourseErrors, Is.EqualTo(courseErrors));
        }
    }
}
using Moq;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Courses.Queries;

namespace CoursesManagement.UnitTests.Courses.Queries.GetCoursesList
{
    [TestFixture]
    public class GetCoursesListQueryHandlerTests
    {
        private Mock<IGetCoursesListService> _service;
        private GetCoursesListQuery _query;
        private GetCoursesListQueryHandler _sut;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IGetCoursesListService>();
            _query = new GetCoursesListQuery();
            _sut = new GetCoursesListQueryHandler(_service.Object);
        }

        [Test]
        public void WhenCalled_ReturnOrganizationCoursesList()
        {
            var queryResult = new CoursesListResult(1, new CoursesListItemDto[1]);
            _service.Setup(x => x.GetCoursesList(_query, default)).ReturnsAsync(queryResult);

            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.QueryResult, Is.EqualTo(queryResult));
        }
    }
}
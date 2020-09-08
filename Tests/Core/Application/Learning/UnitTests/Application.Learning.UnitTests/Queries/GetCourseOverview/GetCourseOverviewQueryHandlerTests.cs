using Moq;
using NUnit.Framework;
using Solen.Core.Application.Learning.Queries;

namespace Application.Learning.UnitTests.Queries.GetCourseOverview
{
    [TestFixture]
    public class GetCourseOverviewQueryHandlerTests
    {
        private Mock<IGetCourseOverviewService> _service;
        private GetCourseOverviewQueryHandler _sut;
        private GetCourseOverviewQuery _query;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IGetCourseOverviewService>();
            _sut = new GetCourseOverviewQueryHandler(_service.Object);
            _query = new GetCourseOverviewQuery {CourseId = "courseId"};
        }
        
        [Test]
        public void WhenCalled_ReturnCorrectCourseOverview()
        {
            var courseOverview = new LearnerCourseOverviewDto();
            _service.Setup(x => x.GetCourseOverview(_query.CourseId, default))
                .ReturnsAsync(courseOverview);

            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.CourseOverview, Is.EqualTo(courseOverview));
        }
    }
}
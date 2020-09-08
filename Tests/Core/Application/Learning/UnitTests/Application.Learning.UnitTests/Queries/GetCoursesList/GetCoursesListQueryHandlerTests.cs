using Moq;
using NUnit.Framework;
using Solen.Core.Application.Learning.Queries;

namespace Application.Learning.UnitTests.Queries.GetCoursesList
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
        public void WhenCalled_ReturnLearnerCoursesList()
        {
            var queryResult = new LearnerCoursesListResult(1, new []{new LearnerCourseDto()}, default);
            _service.Setup(x => x.GetCoursesList( _query, default))
                .ReturnsAsync(queryResult);

            var viewModel = _sut.Handle(_query, default).Result;

            Assert.That(viewModel.QueryResult.TotalItems, Is.EqualTo(1));
        }
    }
}
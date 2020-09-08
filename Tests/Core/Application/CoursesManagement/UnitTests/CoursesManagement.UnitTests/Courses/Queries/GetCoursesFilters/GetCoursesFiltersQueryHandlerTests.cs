using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Courses.Queries;

namespace CoursesManagement.UnitTests.Courses.Queries.GetCoursesFilters
{
    [TestFixture]
    public class GetCoursesFiltersQueryHandlerTests
    {
        private Mock<IGetCoursesFiltersService> _service;
        private GetCoursesFiltersQueryHandler _sut;
        private GetCoursesFiltersQuery _query;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IGetCoursesFiltersService>();
            _sut = new GetCoursesFiltersQueryHandler(_service.Object);
            _query = new GetCoursesFiltersQuery();
        }

        [Test]
        public void WhenCalled_ReturnCorrectAuthorsFiltersList()
        {
            var authorsList = new List<CoursesManagementAuthorFilterDto>();
            _service.Setup(x => x.GetCoursesAuthors(default))
                .ReturnsAsync(authorsList);

            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.AuthorsFiltersList, Is.EqualTo(authorsList));
        }

        [Test]
        public void WhenCalled_ReturnCorrectLearningPathsFiltersList()
        {
            var learningPathsFilters = new List<LearningPathFilterDto>();
            _service.Setup(x => x.GetLearningPaths(default))
                .ReturnsAsync(learningPathsFilters);

            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.LearningPathsFiltersList, Is.EqualTo(learningPathsFilters));
        }

        [Test]
        public void WhenCalled_ReturnCorrectOrderByFiltersList()
        {
            var orderByFilters = new List<CoursesManagementOrderByDto>();
            _service.Setup(x => x.GetOrderByValues()).Returns(orderByFilters);

            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.OrderByFiltersList, Is.EqualTo(orderByFilters));
        }

        [Test]
        public void WhenCalled_ReturnCorrectOrderByDefaultValue()
        {
            _service.Setup(x => x.GetOrderByDefaultValue()).Returns(1);

            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.OrderByDefaultValue, Is.EqualTo(1));
        }

        [Test]
        public void WhenCalled_ReturnCorrectStatusFiltersList()
        {
            var statusFilters = new List<StatusFilterDto>();
            _service.Setup(x => x.GetStatus()).Returns(statusFilters);

            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.StatusFiltersList, Is.EqualTo(statusFilters));
        }
    }
}
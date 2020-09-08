using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.Learning.Queries;

namespace Application.Learning.UnitTests.Queries.GetCoursesFilters
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
        public void WhenCalled_ReturnCorrectAuthorsFilters()
        {
            var authorsFilters = new List<LearnerCourseAuthorFilterDto>();
            _service.Setup(x => x.GetCoursesAuthors(default))
                .ReturnsAsync(authorsFilters);

            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.AuthorsFiltersList, Is.EqualTo(authorsFilters));
        }

        [Test]
        public void WhenCalled_ReturnCorrectOrdersByList()
        {
            var orderByList = new List<LearnerCourseOrderByDto>();
            _service.Setup(x => x.GetOrderByValues()).Returns(orderByList);

            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.OrderByFiltersList, Is.EqualTo(orderByList));
        }
        
        [Test]
        public void WhenCalled_ReturnCorrectOrderByDefaultValue()
        {
            var orderByDefaultValue = 1;
            _service.Setup(x => x.GetOrderByDefaultValue()).Returns(orderByDefaultValue);

            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.OrderByDefaultValue, Is.EqualTo(orderByDefaultValue));
        }
    }
}
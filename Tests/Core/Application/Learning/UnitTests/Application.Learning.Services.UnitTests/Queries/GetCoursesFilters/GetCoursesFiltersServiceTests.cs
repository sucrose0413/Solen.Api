using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Learning.Queries;
using Solen.Core.Application.Learning.Services.Queries;
using Solen.Core.Domain.Common;

namespace Application.Learning.Services.UnitTests.Queries.GetCoursesFilters
{
    [TestFixture]
    public class GetCoursesFiltersServiceTests
    {
        private Mock<IGetCoursesFiltersRepository> _repo;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;
        private GetCoursesFiltersService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IGetCoursesFiltersRepository>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();
            _sut = new GetCoursesFiltersService(_repo.Object, _currentUserAccessor.Object);

            _currentUserAccessor.Setup(x => x.OrganizationId).Returns("organizationId");
        }

        [Test]
        public void GetCoursesAuthors_WhenCalled_ReturnCorrectAuthorsFiltersList()
        {
            var authors = new List<LearnerCourseAuthorFilterDto>();
            _repo.Setup(x => x.GetCoursesAuthors("organizationId", default))
                .ReturnsAsync(authors);

            var result = _sut.GetCoursesAuthors(default).Result;

            Assert.That(result, Is.EqualTo(authors));
        }

        [Test]
        public void GetOrderByValues_WhenCalled_ReturnCorrectOrderByValues()
        {
            var orderByList = Enumeration.GetAll<CourseOrderBy>();

            var result = _sut.GetOrderByValues();

            Assert.That(result, Is.TypeOf<List<LearnerCourseOrderByDto>>());
            Assert.That(result.Count, Is.EqualTo(orderByList.Count()));
        }

        [Test]
        public void GetOrderByDefaultValue_WhenCalled_ReturnLastAccessedValue()
        {
            var result = _sut.GetOrderByDefaultValue();

            Assert.That(result, Is.EqualTo(new OrderByLastAccessed().Value));
        }
    }
}
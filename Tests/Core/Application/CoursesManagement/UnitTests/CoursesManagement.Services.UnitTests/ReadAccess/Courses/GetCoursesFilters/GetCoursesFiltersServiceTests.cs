using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.CoursesManagement.Courses.Queries;
using Solen.Core.Application.CoursesManagement.Services.Courses;
using Solen.Core.Domain.Common;
using Solen.Core.Domain.Courses.Enums.CourseStatuses;

namespace CoursesManagement.Services.UnitTests.ReadAccess.Courses.GetCoursesFilters
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
            var authors = new List<CoursesManagementAuthorFilterDto>();
            _repo.Setup(x => x.GetCoursesAuthors("organizationId", default))
                .ReturnsAsync(authors);

            var result = _sut.GetCoursesAuthors(default).Result;

            Assert.That(result, Is.EqualTo(authors));
        }

        [Test]
        public void GetLearningPaths_WhenCalled_ReturnCorrectLearningPathsFiltersList()
        {
            var learningPaths = new List<LearningPathFilterDto>();
            _repo.Setup(x => x.GetLearningPaths("organizationId", default))
                .ReturnsAsync(learningPaths);

            var result = _sut.GetLearningPaths(default).Result;

            Assert.That(result, Is.EqualTo(learningPaths));
        }

        [Test]
        public void GetOrderByValues_WhenCalled_ReturnCorrectOrderByValues()
        {
            var orderByList = Enumeration.GetAll<CourseOrderBy>();

            var result = _sut.GetOrderByValues();

            Assert.That(result, Is.TypeOf<List<CoursesManagementOrderByDto>>());
            Assert.That(result.Count, Is.EqualTo(orderByList.Count()));
        }

        [Test]
        public void GetOrderByDefaultValue_WhenCalled_ReturnCreationDateDescValue()
        {
            var result = _sut.GetOrderByDefaultValue();

            Assert.That(result, Is.EqualTo(new OrderByCreationDateDesc().Value));
        }

        [Test]
        public void GetStatus_WhenCalled_ReturnCorrectStatusValues()
        {
            var statusList = Enumeration.GetAll<CourseStatus>();

            var result = _sut.GetStatus();

            Assert.That(result, Is.TypeOf<List<StatusFilterDto>>());
            Assert.That(result.Count, Is.EqualTo(statusList.Count()));
        }
    }
}
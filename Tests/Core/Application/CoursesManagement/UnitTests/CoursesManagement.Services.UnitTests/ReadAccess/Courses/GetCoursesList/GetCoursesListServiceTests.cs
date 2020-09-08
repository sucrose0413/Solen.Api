using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Courses.Queries;
using Solen.Core.Application.CoursesManagement.Services.Courses;
using Solen.Core.Application.Common.Security;

namespace CoursesManagement.Services.UnitTests.ReadAccess.Courses
{
    [TestFixture]
    public class GetCoursesListServiceTests
    {
        private Mock<IGetCoursesListRepository> _repo;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;
        private GetCoursesListService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IGetCoursesListRepository>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();
            _sut = new GetCoursesListService(_repo.Object, _currentUserAccessor.Object);

            _currentUserAccessor.Setup(x => x.OrganizationId).Returns("organizationId");
        }

        [Test]
        public void GetCoursesList_OrderByNotDefined_SetCreationDateDescOrder()
        {
            var query = new GetCoursesListQuery();

            _sut.GetCoursesList(query, default).Wait();

            Assert.That(query.OrderBy, Is.EqualTo(new OrderByCreationDateDesc().Value));
        }

        [Test]
        public void GetCoursesList_WhenCalled_ReturnCorrectCoursesList()
        {
            var query = new GetCoursesListQuery();
            var coursesList = new CoursesListResult(1, new List<CoursesListItemDto>());
            _repo.Setup(x => x.GetCoursesList(query, "organizationId", default))
                .ReturnsAsync(coursesList);

            var result = _sut.GetCoursesList(query, default).Result;

            Assert.That(result, Is.EqualTo(coursesList));
        }
    }
}
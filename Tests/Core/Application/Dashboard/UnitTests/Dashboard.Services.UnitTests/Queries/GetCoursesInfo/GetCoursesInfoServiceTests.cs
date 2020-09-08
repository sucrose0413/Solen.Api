using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Dashboard.Queries;
using Solen.Core.Application.Dashboard.Services.Queries;
using Solen.Core.Domain.Courses.Enums.CourseStatuses;

namespace Dashboard.Services.UnitTests.Queries.GetCoursesInfo
{
    [TestFixture]
    public class GetCoursesInfoServiceTests
    {
        private Mock<IGetCoursesInfoRepository> _repo;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;
        private GetCoursesInfoService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IGetCoursesInfoRepository>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();
            _sut = new GetCoursesInfoService(_repo.Object, _currentUserAccessor.Object);

            _currentUserAccessor.Setup(x => x.OrganizationId).Returns("organizationId");
        }

        [Test]
        public void GetLastCreatedCourse_WhenCalled_ReturnLastCreatedCourse()
        {
            var lastCreatedCourse = new LastCreatedCourseDto();
            _repo.Setup(x => x.GetLastCreatedCourse("organizationId", default))
                .ReturnsAsync(lastCreatedCourse);

            var result = _sut.GetLastCreatedCourse(default).Result;

            Assert.That(result, Is.EqualTo(lastCreatedCourse));
        }

        [Test]
        public void GetLastPublishedCourse_WhenCalled_ReturnLastPublishedCourse()
        {
            var lastPublishedCourse = new LastPublishedCourseDto();
            _repo.Setup(x => x.GetLastPublishedCourse("organizationId", new PublishedStatus().Name, default))
                .ReturnsAsync(lastPublishedCourse);

            var result = _sut.GetLastPublishedCourse(default).Result;

            Assert.That(result, Is.EqualTo(lastPublishedCourse));
        }

        [Test]
        public void GetCourseCount_WhenCalled_ReturnCourseCount()
        {
            var courseCount = 1;
            _repo.Setup(x => x.GetCourseCount("organizationId", default))
                .ReturnsAsync(courseCount);

            var result = _sut.GetCourseCount(default).Result;

            Assert.That(result, Is.EqualTo(courseCount));
        }
    }
}
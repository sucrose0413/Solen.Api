using Moq;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Services.Courses;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.CoursesManagement.Common;

namespace CoursesManagement.Services.UnitTests.ReadAccess.Courses
{
    [TestFixture]
    public class GetCourseContentServiceTests
    {
        private Mock<IGetCourseContentRepository> _repo;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;
        private GetCourseContentService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IGetCourseContentRepository>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();
            _sut = new GetCourseContentService(_repo.Object, _currentUserAccessor.Object);

            _currentUserAccessor.Setup(x => x.OrganizationId).Returns("organizationId");
        }

        [Test]
        public void GetCourseContent_CourseDoesNotExist_ThrowNotFoundException()
        {
            _repo.Setup(x => x.GetCourseContent("courseId", "organizationId", default))
                .ReturnsAsync((CourseContentDto) null);

            Assert.That(() => _sut.GetCourseContent("courseId", default), Throws.Exception.TypeOf<NotFoundException>());
        }

        [Test]
        public void GetCourseContent_CourseDoesExist_ReturnCorrectCourseContent()
        {
            var course = new CourseContentDto();
            _repo.Setup(x => x.GetCourseContent("courseId", "organizationId", default))
                .ReturnsAsync(course);

            var result = _sut.GetCourseContent("courseId", default).Result;

            Assert.That(result, Is.EqualTo(course));
        }
    }
}
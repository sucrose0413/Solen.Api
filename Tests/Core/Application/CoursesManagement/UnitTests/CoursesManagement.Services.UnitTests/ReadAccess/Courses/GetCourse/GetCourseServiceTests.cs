using Moq;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Services.Courses;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.CoursesManagement.Common;

namespace CoursesManagement.Services.UnitTests.ReadAccess.Courses
{
    [TestFixture]
    public class GetCourseServiceTests
    {
        private Mock<IGetCourseRepository> _repo;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;
        private Mock<ICourseErrorsManager> _courseErrorsManager;
        private GetCourseService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IGetCourseRepository>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();
            _courseErrorsManager = new Mock<ICourseErrorsManager>();
            _sut = new GetCourseService(_repo.Object, _currentUserAccessor.Object, _courseErrorsManager.Object);

            _currentUserAccessor.Setup(x => x.OrganizationId).Returns("organizationId");
        }

        [Test]
        public void GetCourse_CourseDoesNotExist_ThrowNotFoundException()
        {
            _repo.Setup(x => x.GetCourse("courseId", "organizationId", default))
                .ReturnsAsync((CourseDto) null);

            Assert.That(() => _sut.GetCourse("courseId", default), Throws.Exception.TypeOf<NotFoundException>());
        }

        [Test]
        public void GetCourse_CourseDoesExist_ReturnCorrectCourse()
        {
            var course = new CourseDto();
            _repo.Setup(x => x.GetCourse("courseId", "organizationId", default))
                .ReturnsAsync(course);

            var result = _sut.GetCourse("courseId", default).Result;

            Assert.That(result, Is.EqualTo(course));
        }

        [Test]
        public void GetCourseErrors_WhenCalled_ReturnCourseErrors()
        {
            _sut.GetCourseErrors("courseId", default).Wait();

            _courseErrorsManager.Verify(x => x.GetCourseErrors("courseId", default));
        }
    }
}
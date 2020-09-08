using System;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Services.Courses;
using Solen.Core.Application.CoursesManagement.Edit.Services.Exceptions;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Common.Security;
using Solen.Core.Domain.Courses.Entities;

namespace CoursesManagement.Services.UnitTests.EditMode.Courses
{
    [TestFixture]
    public class CoursesCommonServiceTests
    {
        private Mock<ICoursesCommonRepository> _repo;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;
        private CoursesCommonService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<ICoursesCommonRepository>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();
            _sut = new CoursesCommonService(_repo.Object, _currentUserAccessor.Object);

            _currentUserAccessor.Setup(x => x.OrganizationId).Returns("organizationId");
        }

        [Test]
        public void GetCourseFromRepo_CourseDoesNotExist_ThrowNotFoundException()
        {
            _repo.Setup(x => x.FindCourse("courseId", "organizationId", default))
                .ReturnsAsync((Course) null);

            Assert.That(() => _sut.GetCourseFromRepo("courseId", default),
                Throws.Exception.TypeOf<NotFoundException>());
        }

        [Test]
        public void GetCourseFromRepo_CourseDoesExist_ReturnCorrectCourse()
        {
            var course = new Course("title", "creatorId", DateTime.Now);
            _repo.Setup(x => x.FindCourse("courseId", "organizationId", default))
                .ReturnsAsync(course);

            var result = _sut.GetCourseFromRepo("courseId", default).Result;

            Assert.That(result, Is.EqualTo(course));
        }

        [Test]
        public void CheckCourseStatusForModification_CourseNotEditable_ThrowUnalterableCourseException()
        {
            var course = new Mock<Course>("title", "creatorId", DateTime.Now);
            course.Setup(x => x.IsEditable).Returns(false);

            Assert.That(() => _sut.CheckCourseStatusForModification(course.Object),
                Throws.Exception.TypeOf<UnalterableCourseException>());
        }

        [Test]
        public void CheckCourseStatusForModification_CourseIsEditable_NotThrowException()
        {
            var course = new Mock<Course>("title", "creatorId", DateTime.Now);
            course.Setup(x => x.IsEditable).Returns(true);

            Assert.That(() => _sut.CheckCourseStatusForModification(course.Object), Throws.Nothing);
        }

        [Test]
        public void UpdateCourseRepo_WhenCalled_UpdateCourseRepo()
        {
            var course = new Course("title", "creatorId", DateTime.Now);

            _sut.UpdateCourseRepo(course);

            _repo.Verify(x => x.UpdateCourse(course));
        }
    }
}
using System;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Services.Exceptions;
using Solen.Core.Application.CoursesManagement.Edit.Services.Modules;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Common.Security;
using Solen.Core.Domain.Courses.Entities;

namespace CoursesManagement.Services.UnitTests.EditMode.Modules
{
    [TestFixture]
    public class CreateModuleServiceTests
    {
        private Mock<ICreateModuleRepository> _repo;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;
        private CreateModuleService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<ICreateModuleRepository>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();
            _sut = new CreateModuleService(_repo.Object, _currentUserAccessor.Object);

            _currentUserAccessor.Setup(x => x.OrganizationId).Returns("organizationId");
        }


        [Test]
        public void ControlCourseExistenceAndStatus_CourseDoesNotExist_ThrowNotFoundException()
        {
            _repo.Setup(x => x.GetCourse("courseId", "organizationId", default))
                .ReturnsAsync((Course) null);

            Assert.That(() => _sut.ControlCourseExistenceAndStatus("courseId", default),
                Throws.Exception.TypeOf<NotFoundException>());
        }

        [Test]
        public void ControlCourseExistenceAndStatus_CourseIsNotEditable_ThrowUnalterableCourseException()
        {
            // Arrange
            var course = new Mock<Course>("title", "creatorId", DateTime.Now);
            course.Setup(x => x.IsEditable).Returns(false);
            _repo.Setup(x => x.GetCourse("courseId", "organizationId", default))
                .ReturnsAsync(course.Object);

            // Act & Assert
            Assert.That(() => _sut.ControlCourseExistenceAndStatus("courseId", default),
                Throws.Exception.TypeOf<UnalterableCourseException>());
        }

        [Test]
        public void ControlCourseExistenceAndStatus_CourseDoesExitAndIsEditable_NotThrowException()
        {
            // Arrange
            var course = new Mock<Course>("title", "creatorId", DateTime.Now);
            course.Setup(x => x.IsEditable).Returns(true);
            _repo.Setup(x => x.GetCourse("courseId", "organizationId", default))
                .ReturnsAsync(course.Object);

            // Act & Assert
            Assert.That(() => _sut.ControlCourseExistenceAndStatus("courseId", default), Throws.Nothing);
        }

        [Test]
        public void AddModuleToRepo_WhenCalled_AddModuleToRepo()
        {
            var module = new Module("module", "courseId", 1);

            _sut.AddModuleToRepo(module, default);

            _repo.Verify(x => x.AddModule(module, default));
        }
    }
}
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
    public class ModulesCommonServiceTests
    {
        private Mock<IModulesCommonRepository> _repo;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;
        private ModulesCommonService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IModulesCommonRepository>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();
            _sut = new ModulesCommonService(_repo.Object, _currentUserAccessor.Object);

            _currentUserAccessor.Setup(x => x.OrganizationId).Returns("organizationId");
        }
        
        [Test]
        public void GetModuleFromRepo_ModuleDoesNotExist_ThrowNotFoundException()
        {
            _repo.Setup(x => x.GetModuleWithCourse("moduleId", "organizationId", default))
                .ReturnsAsync((Module) null);

            Assert.That(() => _sut.GetModuleFromRepo("moduleId", default),
                Throws.Exception.TypeOf<NotFoundException>());
        }

        [Test]
        public void GetModuleFromRepo_ModuleDoesExist_ReturnCorrectModule()
        {
            var module = new Module("module", "courseId", 1);
            _repo.Setup(x => x.GetModuleWithCourse("moduleId", "organizationId", default))
                .ReturnsAsync(module);

            var result = _sut.GetModuleFromRepo("moduleId", default).Result;

            Assert.That(result, Is.EqualTo(module));
        }

        [Test]
        public void CheckCourseStatusForModification_CourseNotEditable_ThrowUnalterableCourseException()
        {
            // Arrange
            var course = new Mock<Course>("title", "creatorId", DateTime.Now);
            course.Setup(x => x.IsEditable).Returns(false);
            var module = new Module("module", course.Object, 1);

            // Act & Assert
            Assert.That(() => _sut.CheckCourseStatusForModification(module),
                Throws.Exception.TypeOf<UnalterableCourseException>());
        }

        [Test]
        public void CheckCourseStatusForModification_CourseIsEditable_NotThrowException()
        {
            // Arrange
            var course = new Mock<Course>("title", "creatorId", DateTime.Now);
            course.Setup(x => x.IsEditable).Returns(true);
            var module = new Module("module", course.Object, 1);

            // Act & Assert
            Assert.That(() => _sut.CheckCourseStatusForModification(module), Throws.Nothing);
        }
    }
}
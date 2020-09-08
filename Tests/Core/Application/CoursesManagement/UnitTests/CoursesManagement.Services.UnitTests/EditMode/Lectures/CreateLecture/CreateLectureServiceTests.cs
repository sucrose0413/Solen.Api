using System;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Lectures.Commands;
using Solen.Core.Application.CoursesManagement.Edit.Services.Exceptions;
using Solen.Core.Application.CoursesManagement.Edit.Services.Lectures;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Common.Security;
using Solen.Core.Domain.Courses.Entities;

namespace CoursesManagement.Services.UnitTests.EditMode.Lectures
{
    [TestFixture]
    public class CreateLectureServiceTests
    {
        private Mock<ICreateLectureRepository> _repo;
        private Mock<ILectureCreatorFactory> _lectureCreatorFactory;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;
        private CreateLectureService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<ICreateLectureRepository>();
            _lectureCreatorFactory = new Mock<ILectureCreatorFactory>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();
            _sut = new CreateLectureService(_lectureCreatorFactory.Object, _repo.Object, _currentUserAccessor.Object);

            _currentUserAccessor.Setup(x => x.OrganizationId).Returns("organizationId");
        }

        [Test]
        public void ControlModuleExistenceAndCourseStatus_ModuleDoesNotExist_ThrowNotFoundException()
        {
            _repo.Setup(x => x.GetModuleWithCourse("moduleId", "organizationId", default))
                .ReturnsAsync((Module) null);

            Assert.That(() => _sut.ControlModuleExistenceAndCourseStatus("moduleId", default),
                Throws.Exception.TypeOf<NotFoundException>());
        }

        [Test]
        public void ControlModuleExistenceAndCourseStatus_CourseIsNotEditable_ThrowUnalterableCourseException()
        {
            // Arrange
            var course = new Mock<Course>("title", "creatorId", DateTime.Now);
            course.Setup(x => x.IsEditable).Returns(false);
            var module = new Module("module", course.Object, 1);
            _repo.Setup(x => x.GetModuleWithCourse("moduleId", "organizationId", default))
                .ReturnsAsync(module);

            // Act & Assert
            Assert.That(() => _sut.ControlModuleExistenceAndCourseStatus("moduleId", default),
                Throws.Exception.TypeOf<UnalterableCourseException>());
        }

        [Test]
        public void ControlModuleExistenceAndCourseStatus_ModuleDoesExitAndCourseIsEditable_NotThrowException()
        {
            // Arrange
            var course = new Mock<Course>("title", "creatorId", DateTime.Now);
            course.Setup(x => x.IsEditable).Returns(true);
            var module = new Module("module", course.Object, 1);
            _repo.Setup(x => x.GetModuleWithCourse("moduleId", "organizationId", default))
                .ReturnsAsync(module);

            // Act & Assert
            Assert.That(() => _sut.ControlModuleExistenceAndCourseStatus("moduleId", default),
                Throws.Nothing);
        }

        [Test]
        public void CreateLecture_WhenCalled_CreateLecture()
        {
            var lecture = new VideoLecture("title", "moduleId", 1);
            var command = new CreateLectureCommand();
            _lectureCreatorFactory.Setup(x => x.Create(command))
                .Returns(lecture);

            var result = _sut.CreateLecture(command);

            Assert.That(result, Is.EqualTo(lecture));
        }


        [Test]
        public void AddLectureToRepo_WhenCalled_AddTheLectureToRepo()
        {
            var lecture = new ArticleLecture("lecture", "moduleId", 1, "content");

            _sut.AddLectureToRepo(lecture, default);

            _repo.Verify(x => x.AddLecture(lecture, default));
        }
    }
}
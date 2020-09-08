using System;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Services.Exceptions;
using Solen.Core.Application.CoursesManagement.Edit.Services.Lectures;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Common.Security;
using Solen.Core.Domain.Courses.Entities;

namespace CoursesManagement.Services.UnitTests.EditMode.Lectures
{
    [TestFixture]
    public class LecturesCommonServiceTests
    {
        private Mock<ILecturesCommonRepository> _repo;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;
        private LecturesCommonService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<ILecturesCommonRepository>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();
            _sut = new LecturesCommonService(_repo.Object, _currentUserAccessor.Object);

            _currentUserAccessor.Setup(x => x.OrganizationId).Returns("organizationId");
        }
        
        [Test]
        public void GetLectureFromRepo_LectureDoesNotExist_ThrowNotFoundException()
        {
            _repo.Setup(x => x.GetLectureWithCourse("lectureId", "organizationId", default))
                .ReturnsAsync((Lecture) null);

            Assert.That(() => _sut.GetLectureFromRepo("lectureId", default),
                Throws.Exception.TypeOf<NotFoundException>());
        }
        
        [Test]
        public void GetLectureFromRepo_LectureDoesExist_ReturnCorrectLecture()
        {
            var lecture = new ArticleLecture("title", "moduleId", 1, "content");
            _repo.Setup(x => x.GetLectureWithCourse("lectureId", "organizationId", default))
                .ReturnsAsync(lecture);

            var result = _sut.GetLectureFromRepo("lectureId", default).Result;

            Assert.That(result, Is.EqualTo(lecture));
        }

        [Test]
        public void CheckCourseStatusForModification_CourseNotEditable_ThrowUnalterableCourseException()
        {
            // Arrange
            var course = new Mock<Course>("title", "creatorId", DateTime.Now);
            course.Setup(x => x.IsEditable).Returns(false);
            var module = new Module("module", course.Object, 1);
            var lecture = new ArticleLecture("lecture", module, 1, "content");

            // Act & Assert
            Assert.That(() => _sut.CheckCourseStatusForModification(lecture),
                Throws.Exception.TypeOf<UnalterableCourseException>());
        }

        [Test]
        public void CheckCourseStatusForModification_CourseIsEditable_NotThrowException()
        {
            // Arrange
            var course = new Mock<Course>("title", "creatorId", DateTime.Now);
            course.Setup(x => x.IsEditable).Returns(true);
            var module = new Module("module", course.Object, 1);
            var lecture = new ArticleLecture("lecture", module, 1, "content");

            // Act & Assert
            Assert.That(() => _sut.CheckCourseStatusForModification(lecture), Throws.Nothing);
        }
    }
}
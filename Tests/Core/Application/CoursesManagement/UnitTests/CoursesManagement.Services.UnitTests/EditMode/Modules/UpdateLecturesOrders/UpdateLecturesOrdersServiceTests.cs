using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Modules.Commands;
using Solen.Core.Application.CoursesManagement.Edit.Services.Modules;
using Solen.Core.Domain.Courses.Entities;

namespace CoursesManagement.Services.UnitTests.EditMode.Modules
{
    [TestFixture]
    public class UpdateLecturesOrdersServiceTests
    {
        private Mock<IUpdateLecturesOrdersRepository> _repo;
        private UpdateLecturesOrdersService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IUpdateLecturesOrdersRepository>();
            _sut = new UpdateLecturesOrdersService(_repo.Object);
        }
        
        [Test]
        public void GetModuleLecturesFromRepo_WhenCalled_ReturnModuleLecturesList()
        {
            var lectures = new List<Lecture>();
            _repo.Setup(x => x.GetModuleLectures("moduleId", default))
                .ReturnsAsync(lectures);

            var result = _sut.GetModuleLecturesFromRepo("moduleId", default).Result;

            Assert.That(result, Is.EqualTo(lectures));
        }

        [Test]
        public void UpdateLecturesOrders_WhenCalled_UpdateLecturesOrders()
        {
            // Arrange
            var lecture1 = new ArticleLecture("lecture1", "moduleId", 1, "content");
            var lecture2 = new VideoLecture("lecture2", "moduleId", 2);
            var lecturesToUpdateOrders = new List<Lecture> {lecture1, lecture2};

            var lecturesNewOrders = new[]
            {
                new LectureOrderDto {LectureId = lecture1.Id, Order = 2},
                new LectureOrderDto {LectureId = lecture2.Id, Order = 1}
            };

            // Act
            _sut.UpdateLecturesOrders(lecturesToUpdateOrders, lecturesNewOrders);

            // Assert
            Assert.That(lecture1.Order, Is.EqualTo(2));
            Assert.That(lecture2.Order, Is.EqualTo(1));
        }

        [Test]
        public void UpdateLecturesRepo_WhenCalled_UpdateLecturesRepo()
        {
            var lectures = new[] {new VideoLecture("lecture 1", "moduleId", 1)};

            _sut.UpdateLecturesRepo(lectures);

            _repo.Verify(x => x.UpdateLectures(lectures));
        }
    }
}
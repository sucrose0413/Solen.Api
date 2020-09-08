using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Common.Impl;

namespace CoursesManagement.Common.UnitTests
{
    [TestFixture]
    public class NoMediaErrorsHandlerTests
    {
        private Mock<INoMediaErrorsRepository> _repo;
        private NoMediaErrorsHandler _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<INoMediaErrorsRepository>();
            _sut = new NoMediaErrorsHandler(_repo.Object);
        }
        
        [Test]
        public void GetCourseErrors_SomeMediasHaveNoMediaUrl_ErrorsShouldContainNoMediaError()
        {
            var lectures = new List<LectureInErrorDto> {new LectureInErrorDto("lectureId", "moduleId", 1, 1)};
            _repo.Setup(x => x.GetMediaLecturesWithoutUrl("courseId", default))
                .ReturnsAsync(lectures);

            var result = _sut.GetCourseErrors("courseId", default).Result;

            Assert.That(result.Count(p =>
                p.Error.Contains(new NoMediaError().Name) &&
                p.LectureId == "lectureId" && p.ModuleId == "moduleId"), Is.EqualTo(1));
        }

        [Test]
        public void GetCourseErrors_NoneMediaHasNoUrl_ErrorsShouldNotContainNoMediaError()
        {
            _repo.Setup(x => x.GetMediaLecturesWithoutUrl("courseId", default))
                .ReturnsAsync(new List<LectureInErrorDto>());

            var result = _sut.GetCourseErrors("courseId", default).Result;

            Assert.That(result.Count(p =>
                p.Error.Contains(new NoMediaError().Name)), Is.EqualTo(0));
        }
    }
}
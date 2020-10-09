using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Common.Impl;

namespace CoursesManagement.Common.UnitTests
{
    [TestFixture]
    public class NoContentErrorsHandlerTests
    {
        private Mock<INoContentErrorsRepository> _repo;
        private NoContentErrorsHandler _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<INoContentErrorsRepository>();
            _sut = new NoContentErrorsHandler(_repo.Object);
        }

        [Test]
        public void GetCourseErrors_SomeArticlesHaveNoContent_ErrorsShouldContainNoContentError()
        {
            var lectures = new List<LectureInErrorDto> {new LectureInErrorDto("lectureId", "moduleId", 1, 1)};
            _repo.Setup(x => x.GetArticleLecturesWithoutContent("courseId", default))
                .ReturnsAsync(lectures);

            var result = _sut.GetCourseErrors("courseId", default).Result;

            Assert.That(result.Count(p =>
                p.Error.Contains(NoContentError.Instance.Name) &&
                p.LectureId == "lectureId" && p.ModuleId == "moduleId"), Is.EqualTo(1));
        }

        [Test]
        public void GetCourseErrors_NoneArticleHasNoContent_ErrorsShouldNotContainNoContentError()
        {
            _repo.Setup(x => x.GetArticleLecturesWithoutContent("courseId", default))
                .ReturnsAsync(new List<LectureInErrorDto>());

            var result = _sut.GetCourseErrors("courseId", default).Result;

            Assert.That(result.Count(p =>
                p.Error.Contains(NoContentError.Instance.Name)), Is.EqualTo(0));
        }
    }
}
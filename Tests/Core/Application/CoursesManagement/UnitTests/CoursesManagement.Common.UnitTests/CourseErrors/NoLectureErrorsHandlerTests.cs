using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Common.Impl;

namespace CoursesManagement.Common.UnitTests
{
    [TestFixture]
    public class NoLectureErrorsHandlerTests
    {
        private Mock<INoLectureErrorsRepository> _repo;
        private NoLectureErrorsHandler _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<INoLectureErrorsRepository>();
            _sut = new NoLectureErrorsHandler(_repo.Object);
        }

        [Test]
        public void GetCourseErrors_SomeModulesHaveNoLecture_ErrorsShouldContainNoLectureError()
        {
            var modules = new List<ModuleInErrorDto> {new ModuleInErrorDto("moduleId", 1)};
            _repo.Setup(x => x.GetModulesWithoutLectures("courseId", default))
                .ReturnsAsync(modules);

            var result = _sut.GetCourseErrors("courseId", default).Result;

            Assert.That(result.Count(p =>
                p.Error.Contains(NoLectureError.Instance.Name) && p.ModuleId == "moduleId"), Is.EqualTo(1));
        }

        [Test]
        public void GetCourseErrors_NoneModuleHasNoLecture_ErrorsShouldNotContainNoLectureError()
        {
            _repo.Setup(x => x.GetModulesWithoutLectures("courseId", default))
                .ReturnsAsync(new List<ModuleInErrorDto>());

            var result = _sut.GetCourseErrors("courseId", default).Result;

            Assert.That(result.Count(p =>
                p.Error.Contains(NoLectureError.Instance.Name)), Is.EqualTo(0));
        }
    }
}
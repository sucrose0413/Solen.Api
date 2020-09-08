using System.Linq;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Common.Impl;

namespace CoursesManagement.Common.UnitTests
{
    [TestFixture]
    public class NoModuleErrorHandlerTests
    {
        private Mock<INoModuleErrorRepository> _repo;
        private NoModuleErrorHandler _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<INoModuleErrorRepository>();
            _sut = new NoModuleErrorHandler(_repo.Object);
        }

        [Test]
        public void GetCourseErrors_CourseHasNoModule_ErrorsShouldContainOneNoModuleError()
        {
            _repo.Setup(x => x.DoesCourseHaveModules("courseId", default))
                .ReturnsAsync(false);

            var result = _sut.GetCourseErrors("courseId", default).Result;

            Assert.That(result.Count(p => p.Error.Contains(new NoModuleError().Name)),
                Is.EqualTo(1));
        }

        [Test]
        public void GetCourseErrors_CourseHasModules_ErrorsShouldNotContainOneNoModuleError()
        {
            _repo.Setup(x => x.DoesCourseHaveModules("courseId", default))
                .ReturnsAsync(true);

            var result = _sut.GetCourseErrors("courseId", default).Result;

            Assert.That(result.Count(p => p.Error.Contains(new NoModuleError().Name)),
                Is.EqualTo(0));
        }
    }
}
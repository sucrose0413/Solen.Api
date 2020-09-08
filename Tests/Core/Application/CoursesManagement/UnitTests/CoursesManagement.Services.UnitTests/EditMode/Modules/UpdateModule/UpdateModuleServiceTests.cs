using Moq;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Services.Modules;
using Solen.Core.Domain.Courses.Entities;

namespace CoursesManagement.Services.UnitTests.EditMode.Modules
{
    [TestFixture]
    public class UpdateModuleServiceTests
    {
        private Mock<IUpdateModuleRepository> _repo;
        private UpdateModuleService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IUpdateModuleRepository>();
            _sut = new UpdateModuleService(_repo.Object);
        }

        [Test]
        public void UpdateModuleRepo_WhenCalled_UpdateModuleRepo()
        {
            var module = new Module("module", "courseId", 1);

            _sut.UpdateModuleRepo(module);

            _repo.Verify(x => x.UpdateModule(module));
        }
    }
}
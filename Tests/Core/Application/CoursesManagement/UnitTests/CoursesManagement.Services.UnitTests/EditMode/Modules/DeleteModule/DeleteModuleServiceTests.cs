using Moq;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Services.Modules;
using Solen.Core.Domain.Courses.Entities;

namespace CoursesManagement.Services.UnitTests.EditMode.Modules
{
    [TestFixture]
    public class DeleteModuleServiceTests
    {
        private Mock<IDeleteModuleRepository> _repo;
        private DeleteModuleService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IDeleteModuleRepository>();
            _sut = new DeleteModuleService(_repo.Object);
        }

        [Test]
        public void RemoveModuleFromRepo_WhenCalled_RemoveModuleFromRepo()
        {
            var module = new Module("module", "courseId", 1);

            _sut.RemoveModuleFromRepo(module);

            _repo.Verify(x => x.RemoveModule(module));
        }
    }
}
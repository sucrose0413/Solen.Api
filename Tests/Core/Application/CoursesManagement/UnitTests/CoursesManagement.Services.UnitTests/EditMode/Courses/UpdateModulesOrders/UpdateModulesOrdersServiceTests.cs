using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Courses.Commands;
using Solen.Core.Application.CoursesManagement.Edit.Services.Courses;
using Solen.Core.Domain.Courses.Entities;

namespace CoursesManagement.Services.UnitTests.EditMode.Courses
{
    [TestFixture]
    public class UpdateModulesOrdersServiceTests
    {
        private Mock<IUpdateModulesOrdersRepository> _repo;
        private UpdateModulesOrdersService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IUpdateModulesOrdersRepository>();
            _sut = new UpdateModulesOrdersService(_repo.Object);
        }

        [Test]
        public void GetCourseModulesFromRepo_WhenCalled_ReturnCourseModulesList()
        {
            var modules = new List<Module>();
            _repo.Setup(x => x.GetCourseModules("courseId", default))
                .ReturnsAsync(modules);

            var result = _sut.GetCourseModulesFromRepo("courseId", default).Result;

            Assert.That(result, Is.EqualTo(modules));
        }


        [Test]
        public void UpdateModulesOrders_WhenCalled_UpdateModulesOrders()
        {
            // Arrange
            var module1 = new Module("module1", "courseId", 1);
            var module2 = new Module("module2", "courseId", 2);
            var modulesToUpdateOrders = new List<Module> {module1, module2};

            var modulesNewOrders = new[]
            {
                new ModuleOrderDto {ModuleId = module1.Id, Order = 2},
                new ModuleOrderDto {ModuleId = module2.Id, Order = 1}
            };

            // Act
            _sut.UpdateModulesOrders(modulesToUpdateOrders, modulesNewOrders);

            // Assert
            Assert.That(module1.Order, Is.EqualTo(2));
            Assert.That(module2.Order, Is.EqualTo(1));
        }

        [Test]
        public void UpdateModulesRepo_WhenCalled_UpdateModulesRepo()
        {
            var modules = new[] {new Module("module 1", "courseId", 1)};

            _sut.UpdateModulesRepo(modules);

            _repo.Verify(x => x.UpdateModules(modules));
        }
    }
}
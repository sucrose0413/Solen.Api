using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Modules.Commands;

namespace CoursesManagement.UnitTests.Modules.Commands.DeleteModule
{
    [TestFixture]
    public class DeleteModuleCommandValidationTest
    {
        private DeleteModuleCommandValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new DeleteModuleCommandValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void ModuleIdIsEmptyOrNull_ShouldHaveError(string moduleId)
        {
            var command = new DeleteModuleCommand {ModuleId = moduleId};

            _sut.ShouldHaveValidationErrorFor(x => x.ModuleId, command);
        }
        
        [Test]
        public void ValidModuleId_ShouldNotHaveError()
        {
            var command = new DeleteModuleCommand {ModuleId = "moduleId"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.ModuleId, command);
        }
    }
}
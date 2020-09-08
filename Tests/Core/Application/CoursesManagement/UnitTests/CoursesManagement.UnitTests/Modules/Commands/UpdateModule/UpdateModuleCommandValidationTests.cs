using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Modules.Commands;

namespace CoursesManagement.UnitTests.Modules.Commands.UpdateModule
{
    [TestFixture]
    public class UpdateModuleCommandValidationTests
    {
        private UpdateModuleCommandValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new UpdateModuleCommandValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void ModuleIdIsEmptyOrNull_ShouldHaveError(string moduleId)
        {
            var command = new UpdateModuleCommand {ModuleId = moduleId};

            _sut.ShouldHaveValidationErrorFor(x => x.ModuleId, command);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void NameIsEmptyOrNull_ShouldHaveError(string name)
        {
            var command = new UpdateModuleCommand {Name = name};

            _sut.ShouldHaveValidationErrorFor(x => x.Name, command);
        }

        [Test]
        public void NameLengthOver100_ShouldHaveError()
        {
            var command = new UpdateModuleCommand {Name = new string('*', 101)};

            _sut.ShouldHaveValidationErrorFor(x => x.Name, command);
        }


        [Test]
        public void ValidParameters_ShouldNotHaveErrors()
        {
            var command = new UpdateModuleCommand
            {
                ModuleId = "moduleId", Name = "module name"
            };

            _sut.ShouldNotHaveValidationErrorFor(x => x.ModuleId, command);
            _sut.ShouldNotHaveValidationErrorFor(x => x.Name, command);
        }
    }
}
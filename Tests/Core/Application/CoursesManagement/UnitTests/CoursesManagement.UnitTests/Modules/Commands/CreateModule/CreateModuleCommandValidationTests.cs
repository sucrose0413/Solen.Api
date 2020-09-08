using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Modules.Commands;

namespace CoursesManagement.UnitTests.Modules.Commands.CreateModule
{
    public class CreateModuleCommandValidationTests
    {
        private CreateModuleCommandValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new CreateModuleCommandValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void NameIsNullOrEmpty_ShouldHaveError(string name)
        {
            var command = new CreateModuleCommand {Name = name};

            _sut.ShouldHaveValidationErrorFor(x => x.Name, command);
        }

        [Test]
        public void NameLengthIsOver100_ShouldHaveError()
        {
            var command = new CreateModuleCommand {Name = new string('*', 101)};

            _sut.ShouldHaveValidationErrorFor(x => x.Name, command);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void CourseIdIsNullOrEmpty_ShouldHaveError(string courseId)
        {
            var command = new CreateModuleCommand {CourseId = courseId};

            _sut.ShouldHaveValidationErrorFor(x => x.CourseId, command);
        }


        [Test]
        public void ValidParameters_ShouldNotHaveError()
        {
            var command = new CreateModuleCommand
                {Name = "name", CourseId = "courseId"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.Name, command);
            _sut.ShouldNotHaveValidationErrorFor(x => x.CourseId, command);
        }
    }
}
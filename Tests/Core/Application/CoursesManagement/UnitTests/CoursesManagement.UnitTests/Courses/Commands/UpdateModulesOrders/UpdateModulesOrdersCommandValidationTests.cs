using System.Collections.ObjectModel;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Courses.Commands;

namespace CoursesManagement.UnitTests.Courses.Commands.UpdateModulesOrders
{
    [TestFixture]
    public class UpdateModulesOrdersCommandValidationTests
    {
        private UpdateModulesOrdersCommandValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new UpdateModulesOrdersCommandValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void EmptyOrNullCourseId_ShouldHaveError(string courseId)
        {
            var command = new UpdateModulesOrdersCommand {CourseId = courseId};

            _sut.ShouldHaveValidationErrorFor(x => x.CourseId, command);
        }
        
        [Test]
        public void ModulesOrdersCollectionIsEmpty_ShouldHaveError()
        {
            var command = new UpdateModulesOrdersCommand {ModulesOrders = new Collection<ModuleOrderDto>()};

            _sut.ShouldHaveValidationErrorFor(x => x.CourseId, command);
        }
        
        [Test]
        public void ModulesOrdersCollectionIsNull_ShouldHaveError()
        {
            var command = new UpdateModulesOrdersCommand {ModulesOrders = null};

            _sut.ShouldHaveValidationErrorFor(x => x.CourseId, command);
        }
        
        [Test]
        public void ValidParameters_ShouldNotHaveError()
        {
            var command = new UpdateModulesOrdersCommand
            {
                CourseId = "courseId",
                ModulesOrders = new[]
                {
                    new ModuleOrderDto {ModuleId = "module1", Order = 1},
                    new ModuleOrderDto {ModuleId = "module2", Order = 2}
                }
            };

            _sut.ShouldNotHaveValidationErrorFor(x => x.CourseId, command);
            _sut.ShouldNotHaveValidationErrorFor(x => x.ModulesOrders, command);
        }
    }
}
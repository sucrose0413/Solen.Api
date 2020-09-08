using System.Collections.ObjectModel;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Modules.Commands;

namespace CoursesManagement.UnitTests.Modules.Commands.UpdateLecturesOrders
{
    [TestFixture]
    public class UpdateLecturesOrdersCommandValidationTests
    {
        private UpdateLecturesOrdersCommandValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new UpdateLecturesOrdersCommandValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void ModuleIdIsEmptyOrNull_ShouldHaveError(string moduleId)
        {
            var command = new UpdateLecturesOrdersCommand {ModuleId = moduleId};

            _sut.ShouldHaveValidationErrorFor(x => x.ModuleId, command);
        }

        [Test]
        public void LecturesOrdersCollectionIsEmpty_ShouldHaveError()
        {
            var command = new UpdateLecturesOrdersCommand {LecturesOrders = new Collection<LectureOrderDto>()};

            _sut.ShouldHaveValidationErrorFor(x => x.LecturesOrders, command);
        }

        [Test]
        public void LecturesOrdersCollectionIsNull_ShouldHaveError()
        {
            var command = new UpdateLecturesOrdersCommand {LecturesOrders = null};

            _sut.ShouldHaveValidationErrorFor(x => x.LecturesOrders, command);
        }

        [Test]
        public void ValidParameters_ShouldNotHaveError()
        {
            var command = new UpdateLecturesOrdersCommand
            {
                ModuleId = "moduleId",
                LecturesOrders = new[]
                {
                    new LectureOrderDto {LectureId = "lecture1", Order = 1},
                    new LectureOrderDto {LectureId = "lecture2", Order = 2}
                }
            };

            _sut.ShouldNotHaveValidationErrorFor(x => x.ModuleId, command);
            _sut.ShouldNotHaveValidationErrorFor(x => x.LecturesOrders, command);
        }
    }
}
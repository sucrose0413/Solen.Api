using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Lectures.Commands;

namespace CoursesManagement.UnitTests.Lectures.Commands.CreateLecture
{
    [TestFixture]
    public class CreateLectureCommandValidationTests
    {
        private CreateLectureCommandValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new CreateLectureCommandValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void TitleIsNullOrEmpty_ShouldHaveError(string title)
        {
             var command = new CreateLectureCommand {Title = title};

            _sut.ShouldHaveValidationErrorFor(x => x.Title, command);
        }
        
        [Test]
        public void TitleIsOver100_ShouldHaveError()
        {
            var command = new CreateLectureCommand {Title = new string('*', 101)};

            _sut.ShouldHaveValidationErrorFor(x => x.Title, command);
        }

        
        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void ModuleIdIsNullOrEmpty_ShouldHaveError(string moduleId)
        {
            var command = new CreateLectureCommand {ModuleId = moduleId};

            _sut.ShouldHaveValidationErrorFor(x => x.ModuleId, command);
        }
        
        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void LectureTypeIsNullOrEmpty_ShouldHaveError(string lectureType)
        {
            var command = new CreateLectureCommand {LectureType = lectureType};

            _sut.ShouldHaveValidationErrorFor(x => x.LectureType, command);
        }

        [Test]
        public void ValidParameters_ShouldNotHaveError()
        {
            var command = new CreateLectureCommand
                {Title = "title", ModuleId = "moduleId", LectureType = "Article"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.Title, command);
            _sut.ShouldNotHaveValidationErrorFor(x => x.ModuleId, command);
            _sut.ShouldNotHaveValidationErrorFor(x => x.LectureType, command);
        }
    }
}
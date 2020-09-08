using System.Collections.Generic;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Courses.Commands;

namespace CoursesManagement.UnitTests.Courses.Commands.UpdateCourse
{
    [TestFixture]
    public class UpdateCourseCommandValidationTests
    {
        private UpdateCourseCommandValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new UpdateCourseCommandValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void EmptyOrNullCourseId_ShouldHaveError(string courseId)
        {
            var command = new UpdateCourseCommand {CourseId = courseId};

            _sut.ShouldHaveValidationErrorFor(x => x.CourseId, command);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void EmptyOrNullTitle_ShouldHaveError(string name)
        {
            var command = new UpdateCourseCommand {Title = name};

            _sut.ShouldHaveValidationErrorFor(x => x.Title, command);
        }

        [Test]
        public void TitleLengthOver60_ShouldHaveError()
        {
            var command = new UpdateCourseCommand {Title = new string('*', 61)};

            _sut.ShouldHaveValidationErrorFor(x => x.Title, command);
        }


        [Test]
        public void SubtitleLengthOver120_ShouldHaveError()
        {
            var command = new UpdateCourseCommand {Subtitle = new string('*', 121)};

            _sut.ShouldHaveValidationErrorFor(x => x.Subtitle, command);
        }


        [Test]
        public void CourseLearnedSkillLengthOver150_ShouldHaveError()
        {
            var command = new UpdateCourseCommand {CourseLearnedSkills = new List<string> {new string('*', 151)}};

            _sut.ShouldHaveValidationErrorFor(x => x.CourseLearnedSkills, command);
        }

        [Test]
        public void ValidParameters_ShouldNotHaveErrors()
        {
            var command = new UpdateCourseCommand
            {
                CourseId = "courseId", Description = "description", Title = "course title",
                Subtitle = "course subtitle",
                CourseLearnedSkills = new List<string> {"courseSkill1"}
            };

            _sut.ShouldNotHaveValidationErrorFor(x => x.CourseId, command);
            _sut.ShouldNotHaveValidationErrorFor(x => x.Title, command);
            _sut.ShouldNotHaveValidationErrorFor(x => x.Subtitle, command);
            _sut.ShouldNotHaveValidationErrorFor(x => x.Description, command);
            _sut.ShouldNotHaveValidationErrorFor(x => x.CourseLearnedSkills, command);
        }
    }
}
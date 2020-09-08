using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Services.Courses;
using Solen.Core.Domain.Courses.Entities;

namespace CoursesManagement.Services.UnitTests.EditMode.Courses
{
    [TestFixture]
    public class UpdateCourseServiceTests
    {
        private Mock<IUpdateCourseRepository> _repo;
        private UpdateCourseService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IUpdateCourseRepository>();
            _sut = new UpdateCourseService(_repo.Object);
        }

        [Test]
        public void UpdateCourseTitle_WhenCalled_UpdateCourseTitle()
        {
            var course = new Mock<Course>("title", "creatorId", DateTime.Now);

            _sut.UpdateCourseTitle(course.Object, "new title");

            course.Verify(x => x.UpdateTitle("new title"));
        }

        [Test]
        public void UpdateCourseSubtitle_WhenCalled_UpdateCourseSubtitle()
        {
            var course = new Mock<Course>("title", "creatorId", DateTime.Now);

            _sut.UpdateCourseSubtitle(course.Object, "subtitle");

            course.Verify(x => x.UpdateSubtitle("subtitle"));
        }

        [Test]
        public void UpdateCourseDescription_WhenCalled_UpdateCourseDescription()
        {
            var course = new Mock<Course>("title", "creatorId", DateTime.Now);

            _sut.UpdateCourseDescription(course.Object, "description");

            course.Verify(x => x.UpdateDescription("description"));
        }

        [Test]
        public void RemoveCourseSkillsFromRepo_WhenCalled_RemoveCourseSkillsFromRepo()
        {
            _sut.RemoveCourseSkillsFromRepo("courseId", default);

            _repo.Verify(x => x.RemoveCourseSkills("courseId", default));
        }

        [Test]
        public void AddSkillsToCourse_WhenCalled_AddSkillsToCourse()
        {
            var course = new Mock<Course>("title", "creatorId", DateTime.Now);
            var skills = new List<string> {"skill 1"};

            _sut.AddSkillsToCourse(course.Object, skills);

            course.Verify(x => x.AddLearnedSkill(It.Is<string>(s => skills.Contains(s))),
                Times.Exactly(skills.Count));
        }
    }
}
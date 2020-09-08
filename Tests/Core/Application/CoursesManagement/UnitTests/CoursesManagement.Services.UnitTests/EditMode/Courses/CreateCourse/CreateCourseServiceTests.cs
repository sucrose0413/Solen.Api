using System;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Services.Courses;
using Solen.Core.Application.Common.Security;
using Solen.Core.Domain.Courses.Entities;

namespace CoursesManagement.Services.UnitTests.EditMode.Courses
{
    [TestFixture]
    public class CreateCourseServiceTests
    {
        private Mock<ICreateCourseRepository> _repo;
        private Mock<IDateTime> _dateTime;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;
        private CreateCourseService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<ICreateCourseRepository>();
            _dateTime = new Mock<IDateTime>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();
            _sut = new CreateCourseService(_repo.Object, _currentUserAccessor.Object, _dateTime.Object);

            _currentUserAccessor.Setup(x => x.OrganizationId).Returns("organizationId");
        }

        [Test]
        public void CreateCourse_WhenCalled_CreateCourse()
        {
            var now = DateTime.Now;
            _dateTime.Setup(x => x.Now).Returns(now);
            _currentUserAccessor.Setup(x => x.UserId).Returns("creatorId");

            var result = _sut.CreateCourse("title");

            Assert.That(result.Title, Is.EqualTo("title"));
            Assert.That(result.CreatorId, Is.EqualTo("creatorId"));
            Assert.That(result.CreationDate, Is.EqualTo(now));
        }

        [Test]
        public void AddCourseToRepo_WhenCalled_AddTheCourseToRepo()
        {
            var course = new Course("title", "creatorId", DateTime.Now);

            _sut.AddCourseToRepo(course, default);

            _repo.Verify(x => x.AddCourse(course, default));
        }
    }
}
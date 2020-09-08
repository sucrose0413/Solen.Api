using System;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Services.Courses;
using Solen.Core.Domain.Courses.Entities;

namespace CoursesManagement.Services.UnitTests.EditMode.Courses
{
    [TestFixture]
    public class DeleteCourseServiceTests
    {
        private Mock<IDeleteCourseRepository> _repo;
        private DeleteCourseService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IDeleteCourseRepository>();
            _sut = new DeleteCourseService(_repo.Object);
        }
        
        [Test]
        public void RemoveCourseFromRepo_WhenCalled_RemoveCourseFromRepo()
        {
            var course = new Course("title", "creatorId", DateTime.Now);

            _sut.RemoveCourseFromRepo(course);

            _repo.Verify(x => x.RemoveCourse(course));
        }
    }
}
using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Edit.Services.Courses;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.CoursesManagement.Common;
using Solen.Core.Domain.Courses.Entities;

namespace CoursesManagement.Services.UnitTests.EditMode.Courses.PublishCourse
{
    [TestFixture]
    public class PublishCourseServiceTests
    {
        private Mock<IPublishCourseRepository> _repo;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;
        private Mock<ICourseErrorsManager> _courseErrorsManager;
        private Mock<IDateTime> _dateTime;
        private PublishCourseService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IPublishCourseRepository>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();
            _courseErrorsManager = new Mock<ICourseErrorsManager>();
            _dateTime = new Mock<IDateTime>();
            _sut = new PublishCourseService(_repo.Object, _currentUserAccessor.Object, _courseErrorsManager.Object,
                _dateTime.Object);

            _currentUserAccessor.Setup(x => x.OrganizationId).Returns("organizationId");
        }

        [Test]
        public void GetCourseWithDetailsFromRepo_CourseDoesNotExist_ThrowNotFoundException()
        {
            _repo.Setup(x => x.GetCourseWithDetails("courseId", "organizationId", default))
                .ReturnsAsync((Course) null);

            Assert.That(() => _sut.GetCourseWithDetailsFromRepo("courseId", default),
                Throws.Exception.TypeOf<NotFoundException>());
        }

        [Test]
        public void GetCourseWithDetailsFromRepo_CourseDoesExist_ReturnCorrectCourse()
        {
            var course = new Course("title", "creatorId", DateTime.Now);
            _repo.Setup(x => x.GetCourseWithDetails("courseId", "organizationId", default))
                .ReturnsAsync(course);

            var result = _sut.GetCourseWithDetailsFromRepo("courseId", default).Result;

            Assert.That(result, Is.EqualTo(course));
        }

        [Test]
        public void CheckCourseErrors_CourseHasErrors_ThrowAppBusinessException()
        {
            _courseErrorsManager.Setup(x => x.GetCourseErrors("courseId", default))
                .ReturnsAsync(new List<CourseErrorDto>() {new CourseErrorDto()});

            Assert.That(() => _sut.CheckCourseErrors("courseId", default),
                Throws.Exception.TypeOf<AppBusinessException>());
        }

        [Test]
        public void CheckCourseErrors_CourseHasNoErrors_NotThrowException()
        {
            _courseErrorsManager.Setup(x => x.GetCourseErrors("courseId", default))
                .ReturnsAsync(new List<CourseErrorDto>());

            Assert.That(() => _sut.CheckCourseErrors("courseId", default), Throws.Nothing);
        }
    }
}
using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Learning.Queries;
using Solen.Core.Application.Learning.Services.Queries;
using Solen.Core.Domain.Courses.Enums.CourseStatuses;

namespace Application.Learning.Services.UnitTests.Queries.GetCourseContent
{
    [TestFixture]
    public class GetCourseContentServiceTests
    {
        private Mock<IGetCourseContentRepository> _repo;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;
        private GetCourseContentService _sut;

        private readonly string _publishedStatus = new PublishedStatus().Name;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IGetCourseContentRepository>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();
            _sut = new GetCourseContentService(_repo.Object, _currentUserAccessor.Object);

            _currentUserAccessor.Setup(x => x.UserId).Returns("learnerId");
            _currentUserAccessor.Setup(x => x.LearningPathId).Returns("learningPathId");
        }

        [Test]
        public void GetCourseContentFromRepo_CourseDoesNotExist_ThrowNotFoundException()
        {
            _repo.Setup(x => x.GetCourseContentFromRepo("courseId", "learningPathId", _publishedStatus, default))
                .ReturnsAsync((LearnerCourseContentDto) null);

            Assert.That(() => _sut.GetCourseContentFromRepo("courseId", default),
                Throws.Exception.TypeOf<NotFoundException>());
        }

        [Test]
        public void GetCourseContentFromRepo_CourseDoesExist_ReturnCorrectCourseContent()
        {
            var course = new LearnerCourseContentDto();
            _repo.Setup(x => x.GetCourseContentFromRepo("courseId", "learningPathId", _publishedStatus, default))
                .ReturnsAsync(course);

            var result = _sut.GetCourseContentFromRepo("courseId", default).Result;

            Assert.That(result, Is.EqualTo(course));
        }

        [Test]
        public void GetLastLectureId_WhenCalled_ReturnTheLearnerLastLectureIdOfTheGivenCourse()
        {
            var lastLectureId = "lastLectureId";
            _repo.Setup(x => x.GetLastLectureId("courseId", "learnerId", default))
                .ReturnsAsync(lastLectureId);

            var result = _sut.GetLastLectureId("courseId", default).Result;

            Assert.That(result, Is.EqualTo(lastLectureId));
        }
    }
}
using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Learning.Queries;
using Solen.Core.Application.Learning.Services.Queries;
using Solen.Core.Domain.Courses.Enums.CourseStatuses;

namespace Application.Learning.Services.UnitTests.Queries.GetCourseOverview
{
    [TestFixture]
    public class GetCourseOverviewServiceTests
    {
        private Mock<IGetCourseOverviewRepository> _repo;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;
        private GetCourseOverviewService _sut;

        private readonly string _publishedStatus = new PublishedStatus().Name;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IGetCourseOverviewRepository>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();
            _sut = new GetCourseOverviewService(_repo.Object, _currentUserAccessor.Object);

            _currentUserAccessor.Setup(x => x.LearningPathId).Returns("learningPathId");
        }

        [Test]
        public void GetCourseOverview_CourseDoesNotExist_ThrowNotFoundException()
        {
            _repo.Setup(x => x.GetCourseOverview("courseId", "learningPathId", _publishedStatus, default))
                .ReturnsAsync((LearnerCourseOverviewDto) null);

            Assert.That(() => _sut.GetCourseOverview("courseId", default),
                Throws.Exception.TypeOf<NotFoundException>());
        }

        [Test]
        public void GetCourseContentFromRepo_CourseDoesExist_ReturnCorrectCourseContent()
        {
            var course = new LearnerCourseOverviewDto();
            _repo.Setup(x => x.GetCourseOverview("courseId", "learningPathId", _publishedStatus, default))
                .ReturnsAsync(course);

            var result = _sut.GetCourseOverview("courseId", default).Result;

            Assert.That(result, Is.EqualTo(course));
        }
    }
}
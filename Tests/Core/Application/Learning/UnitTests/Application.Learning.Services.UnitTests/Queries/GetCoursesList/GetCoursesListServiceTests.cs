using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.Learning.Queries;
using Solen.Core.Application.Learning.Services.Queries;
using Solen.Core.Domain.Courses.Enums.CourseStatuses;

namespace Application.Learning.Services.UnitTests.Queries.GetCoursesList
{
    [TestFixture]
    public class GetCoursesListServiceTests
    {
        private Mock<IGetCoursesListRepository> _repo;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;
        private GetCoursesListService _sut;

        private readonly string _publishedStatus = new PublishedStatus().Name;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IGetCoursesListRepository>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();
            _sut = new GetCoursesListService(_repo.Object, _currentUserAccessor.Object);

            _currentUserAccessor.Setup(x => x.UserId).Returns("learnerId");
            _currentUserAccessor.Setup(x => x.LearningPathId).Returns("learningPathId");
        }

        [Test]
        public void GetCoursesList_OrderByNotDefined_SetLastAccessedOrder()
        {
            var query = new GetCoursesListQuery();

            _sut.GetCoursesList(query, default).Wait();

            Assert.That(query.OrderBy, Is.EqualTo(new OrderByLastAccessed().Value));
        }

        [Test]
        public void GetCoursesList_WhenCalled_ReturnCorrectCoursesList()
        {
            var query = new GetCoursesListQuery();
            var coursesList = new LearnerCoursesListResult(1, new List<LearnerCourseDto>(),
                new List<LearnerCourseProgressDto>());
            _repo.Setup(x => x.GetCoursesList(query, "learnerId", "learningPathId", _publishedStatus, default))
                .ReturnsAsync(coursesList);

            var result = _sut.GetCoursesList(query, default).Result;

            Assert.That(result, Is.EqualTo(coursesList));
        }
    }
}
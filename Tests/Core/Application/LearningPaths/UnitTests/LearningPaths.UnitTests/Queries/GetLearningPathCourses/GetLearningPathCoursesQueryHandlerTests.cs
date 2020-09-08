using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.LearningPaths.Queries;

namespace LearningPaths.UnitTests.Queries.GetLearningPathCourses
{
    [TestFixture]
    public class GetLearningPathCoursesQueryHandlerTests
    {
        private GetLearningPathCoursesQueryHandler _sut;
        private Mock<IGetLearningPathCoursesService> _service;
        private GetLearningPathCoursesQuery _query;


        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IGetLearningPathCoursesService>();
            _sut = new GetLearningPathCoursesQueryHandler(_service.Object);

            _query = new GetLearningPathCoursesQuery("learnerId");
        }

        [Test]
        public void WhenCalled_ReturnLearningPathCourses()
        {
            var learningPathCourses = new List<CourseForLearningPathDto>();
            _service.Setup(x => x.GetLearningPathCourses(_query.LearningPathId, default))
                .ReturnsAsync(learningPathCourses);

            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.Courses, Is.EqualTo(learningPathCourses));
        }
    }
}
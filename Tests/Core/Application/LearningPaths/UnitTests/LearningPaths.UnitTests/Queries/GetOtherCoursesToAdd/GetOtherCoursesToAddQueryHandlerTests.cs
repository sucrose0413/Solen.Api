using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.LearningPaths.Queries;

namespace LearningPaths.UnitTests.Queries.GetOtherCoursesToAdd
{
    [TestFixture]
    public class GetOtherCoursesToAddQueryHandlerTests
    {
        private GetOtherCoursesToAddQueryHandler _sut;
        private Mock<IGetOtherCoursesToAddService> _service;
        private GetOtherCoursesToAddQuery _query;


        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IGetOtherCoursesToAddService>();
            _sut = new GetOtherCoursesToAddQueryHandler(_service.Object);

            _query = new GetOtherCoursesToAddQuery("learnerId");
        }

        [Test]
        public void WhenCalled_ReturnCoursesAvailableListToEventuallyAddToTheLearningPath()
        {
            var availableCoursesToAdd = new List<CourseForLearningPathDto>();
            _service.Setup(x => x.GetOtherCoursesToAdd(_query.LearningPathId, default))
                .ReturnsAsync(availableCoursesToAdd);

            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.Courses, Is.EqualTo(availableCoursesToAdd));
        }
    }
}
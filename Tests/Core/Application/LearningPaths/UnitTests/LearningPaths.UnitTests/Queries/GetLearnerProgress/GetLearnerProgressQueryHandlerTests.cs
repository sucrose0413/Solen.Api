using Moq;
using NUnit.Framework;
using Solen.Core.Application.LearningPaths.Queries;
using Solen.Core.Domain.Identity.Entities;

namespace LearningPaths.UnitTests.Queries.GetLearnerProgress
{
    [TestFixture]
    public class GetLearnerProgressQueryHandlerTests
    {
        private GetLearnerProgressQueryHandler _sut;
        private Mock<IGetLearnerProgressService> _service;
        private GetLearnerProgressQuery _query;

        private User _learner;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IGetLearnerProgressService>();
            _sut = new GetLearnerProgressQueryHandler(_service.Object);

            _query = new GetLearnerProgressQuery("learnerId");

            _learner = new User("email", "organizationId");
            _service.Setup(x => x.GetLearner(_query.LearnerId, default))
                .ReturnsAsync(_learner);
        }

        [Test]
        public void WhenCalled_ReturnLearnerCompletedCourses()
        {
            var completedCourses = new LearnerCompletedCoursesDto();
            _service.Setup(x => x.GetLearnerCompletedCourses(_learner, default))
                .ReturnsAsync(completedCourses);

            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.LearnerCompletedCourses, Is.EqualTo(completedCourses));
        }
    }
}
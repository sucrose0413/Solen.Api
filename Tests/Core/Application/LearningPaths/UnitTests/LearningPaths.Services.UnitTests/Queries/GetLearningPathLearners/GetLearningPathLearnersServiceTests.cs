using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.LearningPaths.Queries;
using Solen.Core.Application.LearningPaths.Services.Queries;

namespace LearningPaths.Services.UnitTests.Queries.GetLearningPathLearners
{
    [TestFixture]
    public class GetLearningPathLearnersServiceTests
    {
        private Mock<IGetLearningPathLearnersRepository> _repo;
        private GetLearningPathLearnersService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IGetLearningPathLearnersRepository>();
            _sut = new GetLearningPathLearnersService(_repo.Object);
        }
        
        [Test]
        public void GetLearningPathCourses_WhenCalled_ReturnTheLearningPathCourses()
        {
            var learners = new List<LearnerForLearningPathDto>();
            _repo.Setup(x => x.GetLearningPathLearners("learningPathId", default))
                .ReturnsAsync(learners);

            var result = _sut.GetLearningPathLearners("learningPathId", default).Result;
            
            Assert.That(result, Is.EqualTo(learners));
        }
    }
}
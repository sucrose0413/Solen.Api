using Moq;
using NUnit.Framework;
using Solen.Core.Application.LearningPaths.Queries;

namespace LearningPaths.UnitTests.Queries.GetLearningPath
{
    [TestFixture]
    public class GetLearningPathQueryHandlerTests
    {
        private GetLearningPathQueryHandler _sut;
        private Mock<IGetLearningPathService> _service;
        private GetLearningPathQuery _query;


        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IGetLearningPathService>();
            _sut = new GetLearningPathQueryHandler(_service.Object);

            _query = new GetLearningPathQuery("learnerId");
        }

        [Test]
        public void WhenCalled_ReturnLearningPath()
        {
            var learningPathDto = new LearningPathDto();
            _service.Setup(x => x.GetLearningPath(_query.LearningPathId, default))
                .ReturnsAsync(learningPathDto);

            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.LearningPath, Is.EqualTo(learningPathDto));
        }
    }
}
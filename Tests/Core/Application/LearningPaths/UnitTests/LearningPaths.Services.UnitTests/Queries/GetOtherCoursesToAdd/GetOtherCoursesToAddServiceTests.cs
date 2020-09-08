using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.LearningPaths.Queries;
using Solen.Core.Application.LearningPaths.Services.Queries;

namespace LearningPaths.Services.UnitTests.Queries.GetOtherCoursesToAdd
{
    [TestFixture]
    public class GetOtherCoursesToAddServiceTests
    {
        private Mock<IGetOtherCoursesToAddRepository> _repo;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;
        private GetOtherCoursesToAddService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IGetOtherCoursesToAddRepository>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();
            _sut = new GetOtherCoursesToAddService(_repo.Object, _currentUserAccessor.Object);

            _currentUserAccessor.Setup(x => x.OrganizationId).Returns("organizationId");
        }

        [Test]
        public void GetOtherCoursesToAdd_WhenCalled_ReturnCoursesThatCanBeAddedToTheLearningPath()
        {
            var courses = new List<CourseForLearningPathDto>();
            _repo.Setup(x => x.GetCoursesToAdd("learningPathId", "organizationId", default))
                .ReturnsAsync(courses);

            var result = _sut.GetOtherCoursesToAdd("learningPathId", default).Result;

            Assert.That(result, Is.EqualTo(courses));
        }
    }
}
using Moq;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Services.LearningPaths;
using Solen.Core.Application.Common.Security;


namespace CoursesManagement.Services.UnitTests.ReadAccess.LearningPaths
{
    [TestFixture]
    public class LearningPathsServiceTests
    {
        private Mock<IGetCourseLearningPathsRepository> _repo;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;
        private GetCourseLearningPathsService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IGetCourseLearningPathsRepository>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();
            _sut = new GetCourseLearningPathsService(_repo.Object, _currentUserAccessor.Object);

            _currentUserAccessor.Setup(x => x.OrganizationId).Returns("organizationId");
        }

        [Test]
        public void GetCourseLearningPaths_ReturnCorrectListFromRepo()
        {
            _sut.GetCourseLearningPathsIds("courseId", default).Wait();

            _repo.Verify(x =>
                x.GetCourseLearningPathsIds("courseId", "organizationId", default));
        }


        [Test]
        public void GetOrganizationLearningPaths_ReturnCorrectListFromRepo()
        {
            _sut.GetOrganizationLearningPaths(default).Wait();

            _repo.Verify(x => x.GetOrganizationLearningPaths("organizationId", default));
        }
    }
}
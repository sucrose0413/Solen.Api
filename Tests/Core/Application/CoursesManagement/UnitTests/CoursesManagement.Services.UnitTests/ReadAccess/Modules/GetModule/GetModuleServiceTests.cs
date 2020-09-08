using Moq;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Services.Modules;
using Solen.Core.Application.Exceptions;
using Solen.Core.Application.Common.Security;
using Solen.Core.Application.CoursesManagement.Common;

namespace CoursesManagement.Services.UnitTests.ReadAccess.Modules
{
    [TestFixture]
    public class GetModuleServiceTests
    {
        private Mock<IGetModuleRepository> _repo;
        private Mock<ICurrentUserAccessor> _currentUserAccessor;
        private GetModuleService _sut;

        [SetUp]
        public void SetUp()
        {
            _repo = new Mock<IGetModuleRepository>();
            _currentUserAccessor = new Mock<ICurrentUserAccessor>();
            _sut = new GetModuleService(_repo.Object, _currentUserAccessor.Object);

            _currentUserAccessor.Setup(x => x.OrganizationId).Returns("organizationId");
        }

        [Test]
        public void GetModule_ModuleDoesNotExist_ThrowNotFoundException()
        {
            _repo.Setup(x => x.GetModule("moduleId", "organizationId", default))
                .ReturnsAsync((ModuleDto) null);

            Assert.That(() => _sut.GetModule("moduleId", default), Throws.Exception.TypeOf<NotFoundException>());
        }

        [Test]
        public void GetModule_ModuleDoesExist_ReturnCorrectModule()
        {
            var module = new ModuleDto();
            _repo.Setup(x => x.GetModule("moduleId", "organizationId", default))
                .ReturnsAsync(module);

            var result = _sut.GetModule("moduleId", default).Result;

            Assert.That(result, Is.EqualTo(module));
        }
    }
}
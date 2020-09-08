using Moq;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Common;
using Solen.Core.Application.CoursesManagement.Modules.Queries;

namespace CoursesManagement.UnitTests.Modules.Queries.GetModule
{
    [TestFixture]
    public class GetModuleQueryHandlerTests
    {
        private GetModuleQueryHandler _sut;
        private GetModuleQuery _query;
        private Mock<IGetModuleService> _service;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IGetModuleService>();
            _query = new GetModuleQuery {ModuleId = "moduleId"};
            _sut = new GetModuleQueryHandler(_service.Object);
        }


        [Test]
        public void WhenCalled_ReturnCorrectModule()
        {
            var module = new ModuleDto();
            _service.Setup(x => x.GetModule(_query.ModuleId, default)).ReturnsAsync(module);

            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.Module, Is.EqualTo(module));
        }
    }
}
using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.CoursesManagement.Modules.Queries;

namespace CoursesManagement.UnitTests.Modules.Queries.GetModule
{
    [TestFixture]
    public class GetModuleQueryValidationTests
    {
        private GetModuleQueryValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new GetModuleQueryValidator();
        }
        
        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void ModuleIdISNullOrEmpty_ShouldHaveError(string moduleId)
        {
            var query = new GetModuleQuery { ModuleId = moduleId};

            _sut.ShouldHaveValidationErrorFor(x => x.ModuleId, query);
        }
        
        [Test]
        public void ValidModuleId_ShouldNotHaveError()
        {
            var query = new  GetModuleQuery { ModuleId = "moduleId"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.ModuleId, query);
        }
    }
}
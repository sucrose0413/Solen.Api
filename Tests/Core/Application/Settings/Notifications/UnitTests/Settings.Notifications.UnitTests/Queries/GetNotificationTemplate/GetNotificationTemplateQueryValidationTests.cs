using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.Settings.Notifications.Queries;

namespace Settings.Notifications.UnitTests.Queries.GetNotificationTemplate
{
    [TestFixture]
    public class GetNotificationTemplateQueryValidationTests
    {
        private GetNotificationTemplateQueryValidator _sut;
        private GetNotificationTemplateQuery _query;

        [SetUp]
        public void SetUp()
        {
            _sut = new GetNotificationTemplateQueryValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void LearningPathIdIsNullOrEmpty_ShouldHaveError(string templateId)
        {
            _query = new GetNotificationTemplateQuery(templateId);

            _sut.ShouldHaveValidationErrorFor(x => x.TemplateId, _query);
        }

        [Test]
        public void LearningPathIdIsValid_ShouldNotHaveError()
        {
            _query = new GetNotificationTemplateQuery("templateId");

            _sut.ShouldNotHaveValidationErrorFor(x => x.TemplateId, _query);
        }
    }
}
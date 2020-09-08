using Moq;
using NUnit.Framework;
using Solen.Core.Application.Settings.Notifications.Queries;

namespace Settings.Notifications.UnitTests.Queries.GetNotificationTemplate
{
    [TestFixture]
    public class GetNotificationTemplateQueryHandlerTests
    {
        private GetNotificationTemplateQueryHandler _sut;
        private Mock<IGetNotificationTemplateService> _service;
        private GetNotificationTemplateQuery _query;


        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IGetNotificationTemplateService>();
            _sut = new GetNotificationTemplateQueryHandler(_service.Object);

            _query = new GetNotificationTemplateQuery("templateId");
        }

        [Test]
        public void WhenCalled_ReturnNotificationTemplate()
        {
            var template = new NotificationTemplateDto();
            _service.Setup(x => x.GetNotificationTemplate(_query.TemplateId, default))
                .ReturnsAsync(template);

            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.NotificationTemplate, Is.EqualTo(template));
        }
    }
}
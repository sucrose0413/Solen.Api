using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Solen.Core.Application.Settings.Notifications.Queries;

namespace Settings.Notifications.UnitTests.Queries.GetNotificationTemplates
{
    [TestFixture]
    public class GetNotificationTemplatesQueryHandlerTests
    {
        private GetNotificationTemplatesQueryHandler _sut;
        private Mock<IGetNotificationTemplatesService> _service;
        private GetNotificationTemplatesQuery _query;


        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IGetNotificationTemplatesService>();
            _sut = new GetNotificationTemplatesQueryHandler(_service.Object);

            _query = new GetNotificationTemplatesQuery();
        }

        [Test]
        public void WhenCalled_ReturnNotificationTemplate()
        {
            var templates = new List<NotificationTemplateDto>();
            _service.Setup(x => x.GetNotificationTemplates(default))
                .ReturnsAsync(templates);

            var result = _sut.Handle(_query, default).Result;

            Assert.That(result.NotificationTemplates, Is.EqualTo(templates));
        }
    }
}
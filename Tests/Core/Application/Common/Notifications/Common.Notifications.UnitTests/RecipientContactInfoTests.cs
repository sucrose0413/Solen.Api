using NUnit.Framework;
using Solen.Core.Application.Common.Notifications;

namespace Common.Notifications.UnitTests
{
    [TestFixture]
    public class RecipientContactInfoTests
    {
        private RecipientContactInfo _sut;

        [Test]
        public void ConstructorWithUserIdAndEmail_WhenCalled_SetPropertiesCorrectly()
        {
            _sut = new RecipientContactInfo("userId", "email");

            Assert.That(_sut.UserId, Is.EqualTo("userId"));
            Assert.That(_sut.Email, Is.EqualTo("email"));
        }
    }
}
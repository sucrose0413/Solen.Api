using NUnit.Framework;
using Solen.Core.Application.SigningUp.EventsHandlers.Users;

namespace SigningUp.EventsHandlers.UnitTests.Users.SigningUpCompleted
{
    [TestFixture]
    public class UserInfoTests
    {
        private UserInfo _sut;

        [Test]
        public void ConstructorWithUserName_WhenCalled_SetPropertiesCorrectly()
        {
            _sut = new UserInfo("user name");

            Assert.That(_sut.UserName, Is.EqualTo("user name"));
        }
    }
}
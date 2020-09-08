using NUnit.Framework;
using Solen.Core.Application.Users.EventsHandlers.Commands;

namespace UsersManagement.EventsHandlers.UnitTests.Commands.InviteMembers.MembersInvitedEvent
{
    [TestFixture]
    public class SigningUpInfoTests
    {
        private SigningUpInfo _sut;

        [Test]
        public void ConstructorWithInvitedByAndLink_WhenCalled_SetPropertiesCorrectly()
        {
            _sut = new SigningUpInfo("invitee name", "linkToCompleteSigning");

            Assert.That(_sut.InvitedBy, Is.EqualTo("invitee name"));
            Assert.That(_sut.LinkToCompleteSigningUp, Is.EqualTo("linkToCompleteSigning"));
        }
    }
}
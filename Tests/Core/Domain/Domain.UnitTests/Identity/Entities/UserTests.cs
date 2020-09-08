using System.Linq;
using System.Text;
using NUnit.Framework;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Identity.Entities;
using Solen.Core.Domain.Identity.Enums;
using Solen.Core.Domain.Identity.Enums.UserStatuses;

namespace Domain.UnitTests.Identity.Entities
{
    [TestFixture]
    public class UserTests
    {
        private User _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new User("USER@email.com", "organizationId");
        }

        [Test]
        public void
            ConstructorWithEmailOrganizationId_WhenCalled_SetRequiredPropertiesCorrectly()
        {
            Assert.That(_sut.Id, Is.Not.Null);
            Assert.That(_sut.Email, Is.EqualTo("user@email.com"));
            Assert.That(_sut.OrganizationId, Is.EqualTo("organizationId"));
            Assert.That(_sut.CreationDate, Is.Not.Null);
            Assert.That(_sut.UserStatusName, Is.EqualTo(new PendingStatus().Name));
        }


        [Test]
        public void UpdateUserName_WhenCalled_UpdateUserName()
        {
            _sut.UpdateUserName("user name");

            Assert.That(_sut.UserName, Is.EqualTo("user name"));
        }

        [Test]
        public void SetInvitedBy_InvitedByIsAlreadySet_DotNothing()
        {
            _sut.SetInvitedBy("oldInvitedBy");

            _sut.SetInvitedBy("newInvitedBy");

            Assert.That(_sut.InvitedBy, Is.EqualTo("oldInvitedBy"));
        }

        [Test]
        public void SetInvitedBy_InvitedByIsNotSet_SetInvitedByProperty()
        {
            _sut.SetInvitedBy("invitedBy");

            Assert.That(_sut.InvitedBy, Is.EqualTo("invitedBy"));
        }

        [Test]
        public void UpdateLearningPath_WhenCalled_UpdateUserLearningPath()
        {
            var learningPath = new LearningPath("learning Path", "organizationId");

            _sut.UpdateLearningPath(learningPath);

            Assert.That(_sut.LearningPath, Is.EqualTo(learningPath));
            Assert.That(_sut.LearningPathId, Is.EqualTo(learningPath.Id));
        }

        [Test]
        public void UpdatePassword_WhenCalled_UpdateUserPassword()
        {
            var passwordHash = Encoding.ASCII.GetBytes("passwordHash");
            var passwordSalt = Encoding.ASCII.GetBytes("passwordSalt");

            _sut.UpdatePassword(passwordHash, passwordSalt);

            Assert.That(_sut.PasswordHash, Is.EqualTo(passwordHash));
            Assert.That(_sut.PasswordSalt, Is.EqualTo(passwordSalt));
        }

        [Test]
        public void SetInvitationToken_WhenCalled_SetInvitationToken()
        {
            _sut.SetInvitationToken("token");

            Assert.That(_sut.InvitationToken, Is.EqualTo("token"));
        }

        [Test]
        public void InitInvitationToken_WhenCalled_InitInvitationToken()
        {
            _sut.InitInvitationToken();

            Assert.That(_sut.InvitationToken, Is.Null);
        }
        
        [Test]
        public void SetPasswordToken_WhenCalled_SetPasswordForgottenToken()
        {
            _sut.SetPasswordToken("token");

            Assert.That(_sut.PasswordToken, Is.EqualTo("token"));
        }

        [Test]
        public void InitPasswordToken_WhenCalled_InitPasswordToken()
        {
            _sut.InitPasswordToken();

            Assert.That(_sut.PasswordToken, Is.Null);
        }

        [Test]
        public void ChangeUserStatus_WhenCalled_UpdateUserStatus()
        {
            var activeStatus = new ActiveStatus();
            
            _sut.ChangeUserStatus(activeStatus);

            Assert.That(_sut.UserStatusName, Is.EqualTo(activeStatus.Name));
        }

        [Test]
        public void AddRoleId_WhenCalled_AddTheRoleIdToTheUserRolesList()
        {
            _sut.AddRoleId(UserRoles.Learner);

            Assert.That(_sut.UserRoles.Count(x => x.RoleId == UserRoles.Learner), Is.EqualTo(1));
        }
        
        [Test]
        public void AddRole_WhenCalled_AddTheRoleToTheUserRolesList()
        {
            var role = new Role("id", "name", "description");
            
            _sut.AddRole(role);

            Assert.That(_sut.UserRoles.Count(x => x.Role == role), Is.EqualTo(1));
        }

        [Test]
        public void RemoveRole_WhenCalled_RemoveTheRoleFromTheUserRolesList()
        {
            _sut.AddRoleId(UserRoles.Learner);
            
            _sut.RemoveRoleById(UserRoles.Learner);

            Assert.That(_sut.UserRoles.Count(x => x.RoleId == UserRoles.Learner), Is.EqualTo(0));
        }
    }
}
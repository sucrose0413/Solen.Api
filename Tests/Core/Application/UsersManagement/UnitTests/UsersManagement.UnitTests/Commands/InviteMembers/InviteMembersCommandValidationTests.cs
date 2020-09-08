using System.Collections.Generic;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Solen.Core.Application.Users.Commands;

namespace UsersManagement.UnitTests.Commands.InviteMembers
{
    [TestFixture]
    public class InviteMembersCommandValidationTests
    {
        private InviteMembersCommandValidator _sut;
        private InviteMembersCommand _command;

        [SetUp]
        public void SetUp()
        {
            _sut = new InviteMembersCommandValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void LearningPathIdIsNullOrEmpty_ShouldHaveError(string learningPathId)
        {
            _command = new InviteMembersCommand {LearningPathId = learningPathId};

            _sut.ShouldHaveValidationErrorFor(x => x.LearningPathId, _command);
        }

        [Test]
        public void LearningPathIdIsValid_ShouldNotHaveError()
        {
            _command = new InviteMembersCommand {LearningPathId = "learningPathId"};

            _sut.ShouldNotHaveValidationErrorFor(x => x.LearningPathId, _command);
        }

        [Test]
        public void EmailsListIsNull_ShouldHaveError()
        {
            _command = new InviteMembersCommand {Emails = null};

            _sut.ShouldHaveValidationErrorFor(x => x.Emails, _command);
        }

        [Test]
        public void EmailsListIsEmpty_ShouldHaveError()
        {
            _command = new InviteMembersCommand {Emails = new List<string>()};

            _sut.ShouldHaveValidationErrorFor(x => x.Emails, _command);
        }

        [Test]
        public void SomeEmailsAreEmpty_ShouldHaveError()
        {
            _command = new InviteMembersCommand {Emails = new List<string> {""}};

            _sut.ShouldHaveValidationErrorFor(x => x.Emails, _command);
        }
        
        [Test]
        public void SomeEmailsAreNotValidEmailAddress_ShouldHaveError()
        {
            _command = new InviteMembersCommand {Emails = new List<string> {"invalidEmail"}};

            _sut.ShouldHaveValidationErrorFor(x => x.Emails, _command);
        }
        
        [Test]
        public void EmailListIsValid_ShouldNotHaveError()
        {
            _command = new InviteMembersCommand {Emails = new List<string> {"valid@email.com"}};

            _sut.ShouldNotHaveValidationErrorFor(x => x.Emails, _command);
        }
    }
}
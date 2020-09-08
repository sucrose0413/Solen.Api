using System.Collections.Generic;
using System.Linq;
using MediatR;
using Moq;
using Moq.Sequences;
using NUnit.Framework;
using Solen.Core.Application.UnitOfWork;
using Solen.Core.Application.Users.Commands;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Identity.Entities;

namespace UsersManagement.UnitTests.Commands.InviteMembers
{
    [TestFixture]
    public class InviteMembersCommandHandlerTests
    {
        private Mock<IInviteMembersService> _service;
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IMediator> _mediator;
        private InviteMembersCommand _command;
        private InviteMembersCommandHandler _sut;

        private List<User> _usersToInvite;


        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IInviteMembersService>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _mediator = new Mock<IMediator>();
            _sut = new InviteMembersCommandHandler(_service.Object, _unitOfWork.Object, _mediator.Object);

            _command = new InviteMembersCommand {LearningPathId = "learningPathId", Emails = new[] {"email"}};

            _usersToInvite = new List<User> {new User("email", "organizationId")};
            _service.Setup(x => x.CreateUsers(_command.Emails))
                .Returns(_usersToInvite);
        }

        [Test]
        public void WhenCalled_CheckEmailsExistence()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.CheckEmailExistence(It.IsIn(_command.Emails)),
                Times.Exactly(_command.Emails.Count()));
        }

        [Test]
        public void WhenCalled_ForEachInvitedUser_UpdateTheInvitee()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.SetTheInviteeToUser(It.IsIn<User>(_usersToInvite)),
                Times.Exactly(_usersToInvite.Count));
        }

        [Test]
        public void WhenCalled_ForEachInvitedUser_SetTheInvitationToken()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.SetTheInvitationTokenToUser(It.IsIn<User>(_usersToInvite)),
                Times.Exactly(_usersToInvite.Count));
        }

        [Test]
        public void WhenCalled_ForEachInvitedUser_UpdateTheLearningPath()
        {
            var learningPath = new LearningPath("name", "organizationId");
            _service.Setup(x => x.GetLearningPathFromRepo(_command.LearningPathId, default))
                .ReturnsAsync(learningPath);

            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.UpdateUserLearningPath(It.IsIn<User>(_usersToInvite), learningPath),
                Times.Exactly(_usersToInvite.Count));
        }

        [Test]
        public void WhenCalled_ForEachInvitedUser_AddLearnerRole()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.AddLearnerRoleToUser(It.IsIn<User>(_usersToInvite)),
                Times.Exactly(_usersToInvite.Count));
        }

        [Test]
        public void WhenCalled_ForEachInvitedUser_AddTheUserToRepo()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.AddUserToRepo(It.IsIn<User>(_usersToInvite)),
                Times.Exactly(_usersToInvite.Count));
        }

        [Test]
        public void WhenCalled_SaveChanges()
        {
            _sut.Handle(_command, default).Wait();

            _unitOfWork.Verify(x => x.SaveAsync(default));
        }

        [Test]
        public void ChangesSaved_PublishMembersInvitedEvent()
        {
            _sut.Handle(_command, default).Wait();

            _mediator.Verify(x =>
                x.Publish(It.Is<MembersInvitedEvent>(e =>
                    Equals(e.Users, _usersToInvite)), default));
        }

        [Test]
        public void WhenCalled_RespectMethodsCallsOrder()
        {
            using (Sequence.Create())
            {
                _service.Setup(x => x.CheckEmailExistence(It.IsIn(_command.Emails))).InSequence();
                _unitOfWork.Setup(x => x.SaveAsync(default)).InSequence();
                _mediator.Setup(x => x.Publish(It.Is<MembersInvitedEvent>(e =>
                    Equals(e.Users, _usersToInvite)), default)).InSequence();

                _sut.Handle(_command, default).Wait();
            }
        }
    }
}
using System.Collections.Generic;
using Moq;
using Moq.Sequences;
using NUnit.Framework;
using Solen.Core.Application.UnitOfWork;
using Solen.Core.Application.Users.Commands;
using Solen.Core.Domain.Identity.Entities;

namespace UsersManagement.UnitTests.Commands.UpdateUserRoles
{
    [TestFixture]
    public class UpdateUserRolesCommandHandlerTests
    {
        private Mock<IUpdateUserRolesService> _service;
        private Mock<IUnitOfWork> _unitOfWork;
        private UpdateUserRolesCommand _command;
        private UpdateUserRolesCommandHandler _sut;

        private User _userToUpdate;


        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IUpdateUserRolesService>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _sut = new UpdateUserRolesCommandHandler(_service.Object, _unitOfWork.Object);

            _command = new UpdateUserRolesCommand {UserId = "userId", RolesIds = new List<string> {"roleId"}};

            _userToUpdate = new User("email", "organizationId");
            _service.Setup(x => x.GetUserFromRepo(_command.UserId, default))
                .ReturnsAsync(_userToUpdate);
        }

        [Test]
        public void WhenCalled_CheckIfTheUserToBlockIsTheCurrentUser()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.CheckIfTheUserIsTheCurrentUser(_command.UserId));
        }

        [Test]
        public void WhenCalled_CheckRoles()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.CheckRole(It.IsIn<string>(_command.RolesIds), default),
                Times.Exactly(_command.RolesIds.Count));
        }

        [Test]
        public void WhenCalled_RemoveUserExistingRoles()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.RemoveUserRoles(_userToUpdate));
        }

        [Test]
        public void AdminRoleIsIncludedInRolesToAdd_AddOnlyAdminRoleToTheUser()
        {
            _service.Setup(x => x.DoesAdminRoleIncluded(_command.RolesIds))
                .Returns(true);

            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.AddOnlyAdminRoleToUser(_userToUpdate));
            _service.Verify(x => x.AddRolesToUser(_userToUpdate, _command.RolesIds), Times.Never);
        }

        [Test]
        public void AdminRoleIsNotIncludedInRolesToAdd_AddRolesToTheUser()
        {
            _service.Setup(x => x.DoesAdminRoleIncluded(_command.RolesIds))
                .Returns(false);

            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.AddRolesToUser(_userToUpdate, _command.RolesIds));
            _service.Verify(x => x.AddOnlyAdminRoleToUser(_userToUpdate), Times.Never);
        }

        [Test]
        public void WhenCalled_UpdateTheUserRepo()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.UpdateUserRepo(_userToUpdate));
        }

        [Test]
        public void WhenCalled_SaveChanges()
        {
            _sut.Handle(_command, default).Wait();

            _unitOfWork.Verify(x => x.SaveAsync(default));
        }

        [Test]
        public void WhenCalled_RespectMethodsCallsOrder()
        {
            using (Sequence.Create())
            {
                _service.Setup(x => x.CheckIfTheUserIsTheCurrentUser(_command.UserId)).InSequence();
                _service.Setup(x => x.RemoveUserRoles(_userToUpdate)).InSequence();
                _service.Setup(x => x.UpdateUserRepo(_userToUpdate)).InSequence();
                _unitOfWork.Setup(x => x.SaveAsync(default)).InSequence();

                _sut.Handle(_command, default).Wait();
            }
        }
    }
}
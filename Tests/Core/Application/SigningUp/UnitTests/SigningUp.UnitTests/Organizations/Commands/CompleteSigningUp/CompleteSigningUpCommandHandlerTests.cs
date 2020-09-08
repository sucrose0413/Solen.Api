using MediatR;
using Moq;
using Moq.Sequences;
using NUnit.Framework;
using Solen.Core.Application.SigningUp.Organizations.Commands;
using Solen.Core.Application.UnitOfWork;
using Solen.Core.Domain.Common.Entities;
using Solen.Core.Domain.Courses.Entities;
using Solen.Core.Domain.Identity.Entities;
using Solen.Core.Domain.Subscription.Constants;
using Solen.Core.Domain.Subscription.Entities;

namespace SigningUp.UnitTests.Organizations.Commands.CompleteSigningUp
{
    [TestFixture]
    public class CompleteSigningUpCommandHandlerTests
    {
        private Mock<ICompleteOrganizationSigningUpService> _service;
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IMediator> _mediator;
        private CompleteOrganizationSigningUpCommand _command;
        private CompleteSigningUpCommandHandler _sut;

        private OrganizationSigningUp _signingUp;
        private Organization _organization;
        private User _adminUser;
        private LearningPath _generalLearningPath;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<ICompleteOrganizationSigningUpService>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _mediator = new Mock<IMediator>();
            _sut = new CompleteSigningUpCommandHandler(_service.Object, _unitOfWork.Object, _mediator.Object);

            _command = new CompleteOrganizationSigningUpCommand
            {
                SigningUpToken = "Token",
                OrganizationName = "organization name",
                UserName = "user name",
                Password = "password"
            };

            SigninUpSetUp();
            OrganizationSetUp();
            GeneralLearningPathSetUp();
            AdminUserSetUp();
        }

        [Test]
        public void WhenCalled_RemoveSigninUpFromRepo()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.RemoveSigninUpFromRepo(_signingUp));
        }

        [Test]
        public void WhenCalled_AddOrganizationToRepo()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.AddOrganizationToRepo(_organization, default));
        }

        [Test]
        public void WhenCalled_AddGeneralLearningPathToRepo()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.AddGeneralLearningPathToRepo(_generalLearningPath));
        }

        [Test]
        public void WhenCalled_ValidateAdminUserInscription()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.ValidateAdminUserInscription(_adminUser, _command.Password));
        }

        [Test]
        public void WhenCalled_UpdateAdminUserLearningPath()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.UpdateAdminUserLearningPath(_adminUser, _generalLearningPath));
        }

        [Test]
        public void WhenCalled_AddAdminUserToRepo()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.AddAdminUserToRepo(_adminUser, default));
        }

        [Test]
        public void WhenCalled_SaveChanges()
        {
            _sut.Handle(_command, default).Wait();

            _unitOfWork.Verify(x => x.SaveAsync(default));
        }

        [Test]
        public void ChangesSaved_PublishSigningUpCompletedEvent()
        {
            _sut.Handle(_command, default).Wait();

            _mediator.Verify(x => x.Publish(It.IsAny<SigningUpCompletedEvent>(), default));
        }

        [Test]
        public void WhenCalled_RespectMethodsCallsOrder()
        {
            using (Sequence.Create())
            {
                _service.Setup(x => x.RemoveSigninUpFromRepo(_signingUp)).InSequence();
                _service.Setup(x => x.AddOrganizationToRepo(_organization, default)).InSequence();
                _service.Setup(x => x.AddGeneralLearningPathToRepo(_generalLearningPath)).InSequence();
                _service.Setup(x => x.ValidateAdminUserInscription(_adminUser, _command.Password)).InSequence();
                _service.Setup(x => x.UpdateAdminUserLearningPath(_adminUser, _generalLearningPath)).InSequence();
                _service.Setup(x => x.AddAdminUserToRepo(_adminUser, default)).InSequence();
                _unitOfWork.Setup(x => x.SaveAsync(default)).InSequence();
                _mediator.Setup(x => x.Publish(It.IsAny<SigningUpCompletedEvent>(),
                    default)).InSequence();

                _sut.Handle(_command, default).Wait();
            }
        }

        #region Private Methods

        private void SigninUpSetUp()
        {
            _signingUp = new OrganizationSigningUp("email", "token");
            _service.Setup(x => x.GetSigningUp(_command.SigningUpToken, default))
                .ReturnsAsync(_signingUp);
        }

        private void OrganizationSetUp()
        {
            _organization = new Organization(_command.OrganizationName, SubscriptionPlans.Free);
            _service.Setup(x => x.CreateOrganization(_command.OrganizationName))
                .Returns(_organization);
        }

        private void GeneralLearningPathSetUp()
        {
            _generalLearningPath = new LearningPath("General", _organization.Id);
            _service.Setup(x => x.CreateGeneralLearningPath(_organization.Id))
                .Returns(_generalLearningPath);
        }

        private void AdminUserSetUp()
        {
            _adminUser = new User("email", _organization.Id);
            _service.Setup(x => x.CreateAdminUser("email", _command.UserName, _organization.Id))
                .Returns(_adminUser);
        }

        #endregion
    }
}
using Moq;
using Moq.Sequences;
using NUnit.Framework;
using Solen.Core.Application.Settings.Organization.Commands;
using Solen.Core.Application.UnitOfWork;
using Org = Solen.Core.Domain.Common.Entities.Organization;

namespace Settings.Organization.UnitTests.Commands.UpdateOrganizationInfo
{
    [TestFixture]
    public class UpdateOrganizationInfoCommandHandlerTests
    {
        private UpdateOrganizationInfoCommandHandler _sut;
        private Mock<IUpdateOrganizationInfoService> _service;
        private Mock<IUnitOfWork> _unitOfWork;
        private UpdateOrganizationInfoCommand _command;

        private Org _organization;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<IUpdateOrganizationInfoService>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _sut = new UpdateOrganizationInfoCommandHandler(_service.Object, _unitOfWork.Object);

            _command = new UpdateOrganizationInfoCommand {OrganizationName = "new name"};

            _organization = new Org("name", "plan");
            _service.Setup(x => x.GetOrganization(default))
                .ReturnsAsync(_organization);
        }

        [Test]
        public void WhenCalled_UpdateOrganizationName()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.UpdateOrganizationName(_organization, _command.OrganizationName));
        }
        
        [Test]
        public void WhenCalled_UpdateOrganizationRepo()
        {
            _sut.Handle(_command, default).Wait();

            _service.Verify(x => x.UpdateOrganizationRepo(_organization));
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
                _service.Setup(x => x.UpdateOrganizationName(_organization, _command.OrganizationName))
                    .InSequence();
                _service.Setup(x => x.UpdateOrganizationRepo(_organization))
                    .InSequence();
                _unitOfWork.Setup(x => x.SaveAsync(default)).InSequence();

                _sut.Handle(_command, default).Wait();
            }
        }
    }
}
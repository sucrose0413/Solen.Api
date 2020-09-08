using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.UnitOfWork;

namespace Solen.Core.Application.SigningUp.Organizations.Commands
{
    public class CompleteSigningUpCommandHandler : IRequestHandler<CompleteOrganizationSigningUpCommand>
    {
        private readonly ICompleteOrganizationSigningUpService _service;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;


        public CompleteSigningUpCommandHandler(ICompleteOrganizationSigningUpService service, IUnitOfWork unitOfWork,
            IMediator mediator)
        {
            _service = service;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(CompleteOrganizationSigningUpCommand command, CancellationToken token)
        {
            var signingUp = await _service.GetSigningUp(command.SigningUpToken, token);
            _service.RemoveSigninUpFromRepo(signingUp);

            var organization = _service.CreateOrganization(command.OrganizationName);
            await _service.AddOrganizationToRepo(organization, token);

            var generalLearningPath = _service.CreateGeneralLearningPath(organization.Id);
            _service.AddGeneralLearningPathToRepo(generalLearningPath);

            var adminUser = _service.CreateAdminUser(signingUp.Email, command.UserName, organization.Id);
            _service.ValidateAdminUserInscription(adminUser, command.Password);
            _service.UpdateAdminUserLearningPath(adminUser, generalLearningPath);
            await _service.AddAdminUserToRepo(adminUser, token);

            await _unitOfWork.SaveAsync(token);

            await _mediator.Publish(new SigningUpCompletedEvent(organization, adminUser), token);

            return Unit.Value;
        }
    }
}
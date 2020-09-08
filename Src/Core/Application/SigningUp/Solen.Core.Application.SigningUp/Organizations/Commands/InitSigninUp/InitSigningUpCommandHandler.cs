using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.UnitOfWork;


namespace Solen.Core.Application.SigningUp.Organizations.Commands
{
    public class InitSigningUpCommandHandler : IRequestHandler<InitSigningUpCommand>
    {
        private readonly IInitOrganizationSigningUpService _service;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public InitSigningUpCommandHandler(IInitOrganizationSigningUpService service, IUnitOfWork unitOfWork,
            IMediator mediator)
        {
            _service = service;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(InitSigningUpCommand command, CancellationToken token)
        {
            _service.CheckIfSigningUpIsEnabled();
            
            await _service.CheckEmailExistence(command.Email);

            var signingUpToken = _service.InitOrganizationSigningUp(command.Email);

            await _service.AddOrganizationSigningUpToRepo(signingUpToken, token);

            await _unitOfWork.SaveAsync(token);

            await _mediator.Publish(new SigningUpInitializedEvent(signingUpToken), token);

            return Unit.Value;
        }
    }
}
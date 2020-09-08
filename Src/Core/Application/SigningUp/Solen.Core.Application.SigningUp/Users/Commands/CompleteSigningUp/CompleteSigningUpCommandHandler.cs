using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.UnitOfWork;

namespace Solen.Core.Application.SigningUp.Users.Commands
{
    public class CompleteSigningUpCommandHandler : IRequestHandler<CompleteUserSigningUpCommand>
    {
        private readonly ICompleteUserSigningUpService _service;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public CompleteSigningUpCommandHandler(ICompleteUserSigningUpService service, IUnitOfWork unitOfWork,
            IMediator mediator)
        {
            _service = service;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(CompleteUserSigningUpCommand command, CancellationToken token)
        {
            var user = await _service.GetUserByInvitationToken(command.SigningUpToken);
            _service.UpdateUserName(user, command.UserName);
            _service.ValidateUserInscription(user, command.Password);
            _service.UpdateUserRepo(user);

            await _unitOfWork.SaveAsync(token);

            await _mediator.Publish(new UserSigningUpCompletedEventNotification(user), token);

            return Unit.Value;
        }
    }
}
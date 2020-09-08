using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.UnitOfWork;

namespace Solen.Core.Application.Auth.Commands
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand>
    {
        private readonly IForgotPasswordService _service;
        private readonly IUnitOfWork _unitOfWork;

        public ForgotPasswordCommandHandler(IForgotPasswordService service, IUnitOfWork unitOfWork)
        {
            _service = service;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(ForgotPasswordCommand command, CancellationToken token)
        {
            var user = await _service.GetUserFromRepo(command.Email, token);

            _service.SetUserPasswordToken(user);

            _service.UpdateUserRepo(user);

            await _unitOfWork.SaveAsync(token);

            await _service.PublishPasswordTokenSetEvent(user, token);

            return Unit.Value;
        }
    }
}
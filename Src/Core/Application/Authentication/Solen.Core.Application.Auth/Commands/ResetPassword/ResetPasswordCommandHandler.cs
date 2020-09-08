using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Solen.Core.Application.UnitOfWork;

namespace Solen.Core.Application.Auth.Commands
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand>
    {
        private readonly IResetPasswordService _service;
        private readonly IUnitOfWork _unitOfWork;

        public ResetPasswordCommandHandler(IResetPasswordService service, IUnitOfWork unitOfWork)
        {
            _service = service;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(ResetPasswordCommand command, CancellationToken token)
        {
            var user = await _service.GetUserByPasswordToken(command.PasswordToken, token);

            _service.UpdateUserPassword(user, command.NewPassword);

            _service.InitUserPasswordToken(user);

            _service.UpdateUserRepo(user);

            await _unitOfWork.SaveAsync(token);

            return Unit.Value;
        }
    }
}